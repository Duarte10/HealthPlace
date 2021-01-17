import * as React from 'react';
import axios from 'axios';
import './LoginPage.scss';

type LoginPageState = {
    email: string,
    password: string
};

class LoginPage extends React.Component<{ history: any }, LoginPageState> {
    render() {
        if (window.localStorage.getItem('token')) {
            this.props.history.push('/');
            return null;
        }
        return <>
            <div className="form-signin">
                <img className="mb-4" src={require("../../assets/log-in.svg")} alt="" width="72" height="57" />
                <h1 className="h3 mb-3 fw-normal">Please sign in</h1>
                <label htmlFor="inputEmail" className="visually-hidden">Email address</label>
                <input type="email" id="inputEmail" className="form-control" placeholder="Email address" required onChange={e => this.setState({ email: e.target.value })} />
                <label htmlFor="inputPassword" className="visually-hidden">Password</label>
                <input type="password" id="inputPassword" className="form-control" placeholder="Password" required onChange={e => this.setState({ password: e.target.value })} />
                <button className="w-100 btn btn-lg btn-primary" type="button" onClick={() => this.submit()}>Sign in</button>
            </div>
        </>;
    }

    submit() {
        if (!this.state || !this.state.email || !this.state.password) {
            window.alert('Email and Password are required');
            return;
        }
        axios.post('/users/authenticate', {
            email: this.state.email,
            password: this.state.password
        }).then(result => {
            localStorage.setItem("token", result.data as string);
            this.props.history.push('/');
        }).catch(() => window.alert('Invalid email or password'));
    }
}

export default LoginPage;