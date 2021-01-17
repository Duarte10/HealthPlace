import * as React from 'react';
import './LoginPage.scss';

type LoginPageState = {
    email: string,
    password: string
};

class LoginPage extends React.Component<{ history: any }, LoginPageState> {
    render() {
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

        const body = JSON.stringify({
            email: this.state.email,
            password: this.state.password
        });
        fetch('https://localhost:44374/api/users/authenticate', {
            method: 'post',
            body,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => {
            if (!response.ok) {
                window.alert('Invalid email or password');
            } else {
                return response.text() as Promise<string>;
            }
        }).then(data => {
            localStorage.setItem("token", data);
            this.props.history.push('/')
        })
    }
}

export default LoginPage;