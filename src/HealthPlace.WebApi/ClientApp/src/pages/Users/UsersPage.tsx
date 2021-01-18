import * as React from 'react';
import axios from 'axios';

type User = {
    id: string,
    name: string,
    email: string
}

class UsersPage extends React.Component<{ history: any }, { users: User[], selectedUser?: User, deleteUser: Boolean }> {

    constructor(props: any) {
        super(props);
        this.state = {
            users: [],
            deleteUser: false
        }
    }

    componentDidMount() {
        this.loadUsers();
    }

    loadUsers() {
        axios.get('/users')
            .then(result => this.setState({ users: result.data }));
    }

    openDeleteUserModal(user: User) {
        this.setState({ deleteUser: true, selectedUser: user });
        (document.getElementById('backdrop') as any).style.display = 'block';
        (document.getElementById('deleteUserModal') as any).style.display = 'block';
        (document.getElementById('deleteUserModal') as any).className += 'show';
    }

    closeDeleteUserModal() {
        this.setState({ deleteUser: false, selectedUser: undefined });
        (document.getElementById('backdrop') as any).style.display = 'none';
        (document.getElementById('deleteUserModal') as any).style.display = 'none';
        (document.getElementById('deleteUserModal') as any).className += (document.getElementById('deleteUserModal') as any).className.replace('show', '')
    }

    deleteUser() {
        axios.delete('/users/' + this.state.selectedUser?.id)
            .then(() => {
                this.closeDeleteUserModal();
                this.loadUsers();
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
            <button className='btn btn-primary' style={{ marginBottom: '15px' }} onClick={() => this.props.history.push('/users/new')}>New</button>
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
                        this.state.users.map((u: User) => {
                            return <tr key={u.id}>
                                <td>{u.name}</td>
                                <td>{u.email}</td>
                                <td>
                                    <button className='btn btn-secondary btn-sm' style={{ marginRight: '5px' }}
                                        onClick={() => this.props.history.push('/users/edit/' + u.id)}>
                                        Edit
                                    </button>
                                    <button className='btn btn-secondary btn-sm' onClick={() => this.openDeleteUserModal(u)}>
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        })
                    }
                </tbody>
            </table>
            <div id='deleteUserModal' className='modal fade' tabIndex={-1} role='dialog'>
                <div className='modal-dialog' role='document'>
                    <div className='modal-content'>
                        <div className='modal-header'>
                            <h5 className='modal-title'>Delete user</h5>
                            <button type='button' className='close' data-dismiss='modal' aria-label='Close'>
                                <span aria-hidden='true'>&times;</span>
                            </button>
                        </div>
                        <div className='modal-body'>
                            <p>Are you sure you want to delete the user: {this.state.selectedUser?.name} ?</p>
                        </div>
                        <div className='modal-footer'>
                            <button type='button' className='btn btn-danger'
                                onClick={() => this.deleteUser()}>
                                Delete
                            </button>
                            <button type='button' className='btn btn-secondary'
                                onClick={() => this.closeDeleteUserModal()}>
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

export default UsersPage;
