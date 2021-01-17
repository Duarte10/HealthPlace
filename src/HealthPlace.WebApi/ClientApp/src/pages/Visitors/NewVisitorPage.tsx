import * as React from 'react';
import './NewVisitorPage.scss';

type NewVisitorState = {
    name: string
    email: string,
    mobile: string
};

class NewVisitorPage extends React.Component<{ history: any }, NewVisitorState> {
    render() {
        return <div className="new-visitor">
            <form onSubmit={e => this.submit(e)}>
                <h1 className="h3 mb-3 fw-normal">New user</h1>
                <label htmlFor="inputName" className="visually-hidden">Name</label>
                <input type="text" id="inputName" className="form-control" placeholder="Name" required onChange={e => this.setState({ name: e.target.value })} />
                <label htmlFor="inputEmail" className="visually-hidden">Email address</label>
                <input type="email" id="inputEmail" className="form-control" placeholder="Email address" required onChange={e => this.setState({ email: e.target.value })} />
                <label htmlFor="inputMobile" className="visually-hidden">Mobile</label>
                <input type="text" id="inputMobile" className="form-control" placeholder="Mobile" required onChange={e => this.setState({ mobile: e.target.value })} />
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

        if (!this.state.mobile && !this.state.email) {
            window.alert('Email or mobile required!');
            return;
        }

        const body = JSON.stringify({
            name: this.state.name,
            email: this.state.email,
            mobile: this.state.mobile
        });
        fetch('https://localhost:44374/api/visitors/new', {
            method: 'post',
            body,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => {
            if (!response.ok) {
                window.alert('An error ocurred');
            } else {
                this.props.history.push('/visitors')
            }
        });
    }
}

export default NewVisitorPage;