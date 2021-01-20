import * as React from 'react';
import axios from 'axios';
import { Visitor } from '../../types/Visitor';

class VisitorsPage extends React.Component<{ history: any }, { visitors: Visitor[], selectedVisitor?: Visitor, deleteVisitor: Boolean }> {

    constructor(props: any) {
        super(props);
        this.state = {
            visitors: [],
            deleteVisitor: false
        }
    }

    componentDidMount() {
        this.loadVisitors();
    }

    loadVisitors() {
        axios.get('/visitors')
            .then(result => this.setState({ visitors: result.data }));
    }

    openDeleteVisitorModal(visitor: Visitor) {
        this.setState({ deleteVisitor: true, selectedVisitor: visitor });
        (document.getElementById('backdrop') as any).style.display = 'block';
        (document.getElementById('deleteVisitorModal') as any).style.display = 'block';
        (document.getElementById('deleteVisitorModal') as any).className += 'show';
    }

    closeDeleteVisitorModal() {
        this.setState({ deleteVisitor: false, selectedVisitor: undefined });
        (document.getElementById('backdrop') as any).style.display = 'none';
        (document.getElementById('deleteVisitorModal') as any).style.display = 'none';
        (document.getElementById('deleteVisitorModal') as any).className += (document.getElementById('deleteVisitorModal') as any).className.replace('show', '')
    }

    deleteVisitor() {
        axios.delete('/visitors/' + this.state.selectedVisitor?.id)
            .then(() => {
                this.closeDeleteVisitorModal();
                this.loadVisitors();
            })
            .catch(error => {
                console.error(error);
                if (error.response?.data) {
                    window.alert(error.response.data);
                }
            })
    }

    render() {
        return <>
            <button className='btn btn-primary' style={{ marginBottom: '15px' }} onClick={() => this.props.history.push('/visitors/new')}>New</button>
            <table className='table'>
                <thead>
                    <tr>
                        <th scope='col'>Name</th>
                        <th scope='col'>Mobile</th>
                        <th scope='col'>Email</th>
                        <th scope='col'> </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.visitors.map((v: Visitor) => {
                            return <tr key={v.id}>
                                <td>{v.name}</td>
                                <td>{v.mobile}</td>
                                <td>{v.email}</td>
                                <td>
                                    <button className='btn btn-secondary btn-sm' style={{ marginRight: '5px' }}
                                        onClick={() => this.props.history.push('/visitors/' + v.id + '/overview')}>
                                        View
                                    </button>
                                    <button className='btn btn-secondary btn-sm' style={{ marginRight: '5px' }}
                                        onClick={() => this.props.history.push('/visitors/edit/' + v.id)}>
                                        Edit
                                    </button>
                                    <button className='btn btn-secondary btn-sm' onClick={() => this.openDeleteVisitorModal(v)}>
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        })
                    }
                </tbody>
            </table>
            <div id='deleteVisitorModal' className='modal fade' tabIndex={-1} role='dialog'>
                <div className='modal-dialog' role='document'>
                    <div className='modal-content'>
                        <div className='modal-header'>
                            <h5 className='modal-title'>Delete visitor</h5>
                            <button type='button' className='close' data-dismiss='modal' aria-label='Close'>
                                <span aria-hidden='true'>&times;</span>
                            </button>
                        </div>
                        <div className='modal-body'>
                            <p>Are you sure you want to delete the visitor: {this.state.selectedVisitor?.name} ?</p>
                        </div>
                        <div className='modal-footer'>
                            <button type='button' className='btn btn-danger'
                                onClick={() => this.deleteVisitor()}>
                                Delete
                            </button>
                            <button type='button' className='btn btn-secondary'
                                onClick={() => this.closeDeleteVisitorModal()}>
                                Cancel
                        </button>
                        </div>
                    </div>
                </div>
            </div>
            <div className='modal-backdrop fade show' id='backdrop' style={{ display: 'none' }}></div>
        </>
    }
}

export default VisitorsPage;
