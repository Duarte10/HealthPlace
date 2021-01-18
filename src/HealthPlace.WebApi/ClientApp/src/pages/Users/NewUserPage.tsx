import React from 'react';
import axios from 'axios';


type NewUserState = {
    name: string
    email: string,
    password: string
};

class NewVisitorPage extends React.Component<{ history: any }, NewUserState> {
    render() {
        return <div className="hp-simple-form">
            <form onSubmit={e => this.submit(e)}>
                <h1 className="h3 mb-3 fw-normal">New user</h1>
                <label htmlFor="inputName" className="visually-hidden">Name</label>
                <input type="text" id="inputName" className="form-control" placeholder="Name" required onChange={e => this.setState({ name: e.target.value })} />
                <label htmlFor="inputEmail" className="visually-hidden">Email address</label>
                <input type="email" id="inputEmail" className="form-control" placeholder="Email address" onChange={e => this.setState({ email: e.target.value })} />
                <label htmlFor="inputPassword" className="visually-hidden">Password</label>
                <input type="password" id="inputPassword" className="form-control" placeholder="Password" onChange={e => this.setState({ password: e.target.value })} />
                <button className="w-100 btn btn-lg btn-primary" type="submit">Submit</button>
            </form>
        </div>;
    }

    submit(event: React.FormEvent<HTMLElement>) {
        event.preventDefault();
        if (!this.state || !this.state.name) {
            window.alert('Name is required');
            return;
        }

        if (!this.state.email) {
            window.alert('Email is required');
            return;
        }

        if (!this.state.password) {
            window.alert('Password is required');
            return;
        }
        axios.post('/users/new', {
            name: this.state.name,
            email: this.state.email,
            password: this.state.password
        }).then(() => {
            this.props.history.push('/users');
        }).catch(error => {
            console.error(error);
            if (error.response?.data) {
                window.alert(error.response.data);
            }
        });
    }
}

export default NewVisitorPage;