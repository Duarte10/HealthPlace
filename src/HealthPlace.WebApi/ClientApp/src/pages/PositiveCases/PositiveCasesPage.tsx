import * as React from 'react';
import axios from 'axios';
import { PositiveCase } from '../../types/PositiveCase';
import moment from 'moment';

class PositiveCasesPage extends React.Component<{ history: any }, { positiveCases: PositiveCase[], selectedPositiveCase?: PositiveCase, deletePositiveCase: Boolean }> {

    constructor(props: any) {
        super(props);
        this.state = {
            positiveCases: [],
            deletePositiveCase: false
        }
    }

    componentDidMount() {
        this.loadPositiveCases();
    }

    loadPositiveCases() {
        axios.get('/positive-cases')
            .then(result =>
                this.setState({
                    positiveCases: result.data.map((p: PositiveCase) => {
                        return {
                            ...p,
                            visitDate: moment(p.visitDate).format('DD/MM/YYYY HH:mm')
                        }
                    })
                })
            );
    }

    openDeletePositiveCaseModal(positiveCase: PositiveCase) {
        this.setState({ deletePositiveCase: true, selectedPositiveCase: positiveCase });
        (document.getElementById('backdrop') as any).style.display = 'block';
        (document.getElementById('deletePositiveCaseModal') as any).style.display = 'block';
        (document.getElementById('deletePositiveCaseModal') as any).className += 'show';
    }

    closeDeletePositiveCaseModal() {
        this.setState({ deletePositiveCase: false, selectedPositiveCase: undefined });
        (document.getElementById('backdrop') as any).style.display = 'none';
        (document.getElementById('deletePositiveCaseModal') as any).style.display = 'none';
        (document.getElementById('deletePositiveCaseModal') as any).className += (document.getElementById('deletePositiveCaseModal') as any).className.replace('show', '')
    }

    deletePositiveCase() {
        axios.delete('/positive-cases/' + this.state.selectedPositiveCase?.id)
            .then(() => {
                this.closeDeletePositiveCaseModal();
                this.loadPositiveCases();
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
            <button className='btn btn-primary' style={{ marginBottom: '15px' }} onClick={() => this.props.history.push('/positive-cases/new')}>New</button>
            <table className='table'>
                <thead>
                    <tr>
                        <th scope='col'>Date</th>
                        <th scope='col'>Visitor</th>
                        <th scope='col'>All Users Notified</th>
                        <th scope='col'> </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.positiveCases.map((p: PositiveCase) => {
                            return <tr key={p.id}>
                                <td>{p.visitDate}</td>
                                <td><a href={'/visitors/' + p.visitorId + '/overview'}>Visitor</a></td>
                                <td>{p.allUsersNotified ? 'Yes' : 'No'}</td>
                                <td>
                                    <button className='btn btn-secondary btn-sm' style={{ marginRight: '5px' }}
                                        onClick={() => this.props.history.push('/positive-cases/' + p.id + '/overview')}>
                                        View
                                    </button>
                                    <button className='btn btn-secondary btn-sm' style={{ marginRight: '5px' }}
                                        onClick={() => this.props.history.push('/positive-cases/edit/' + p.id)}>
                                        Edit
                                    </button>
                                    <button className='btn btn-secondary btn-sm' onClick={() => this.openDeletePositiveCaseModal(p)}>
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        })
                    }
                </tbody>
            </table>
            <div id='deletePositiveCaseModal' className='modal fade' tabIndex={-1} role='dialog'>
                <div className='modal-dialog' role='document'>
                    <div className='modal-content'>
                        <div className='modal-header'>
                            <h5 className='modal-title'>Delete positive case</h5>
                            <button type='button' className='close' data-dismiss='modal' aria-label='Close'>
                                <span aria-hidden='true'>&times;</span>
                            </button>
                        </div>
                        <div className='modal-body'>
                            <p>Are you sure you want to delete this positive case?</p>
                        </div>
                        <div className='modal-footer'>
                            <button type='button' className='btn btn-danger'
                                onClick={() => this.deletePositiveCase()}>
                                Delete
                            </button>
                            <button type='button' className='btn btn-secondary'
                                onClick={() => this.closeDeletePositiveCaseModal()}>
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

export default PositiveCasesPage;
