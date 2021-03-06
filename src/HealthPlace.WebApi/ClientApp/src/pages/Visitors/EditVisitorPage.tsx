import * as React from 'react';
import axios from 'axios';
import { RouteComponentProps } from 'react-router';

type EditVisitorState = {
    id: string;
    name: string;
    email: string;
    mobile: string;
};

class EditVisitorPage extends React.Component<RouteComponentProps<{ id: string }>, EditVisitorState> {

    constructor(props: RouteComponentProps<{ id: string }>) {
        super(props);
        this.state = {
            id: '',
            name: '',
            email: '',
            mobile: ''
        };
    }

    componentDidMount() {
        // load user data
        axios.get('visitors/' + this.props.match.params.id)
            .then(result => {
                this.setState({
                    id: result.data.id,
                    name: result.data.name,
                    email: result.data.email ? result.data.email : '',
                    mobile: result.data.mobile ? result.data.mobile : ''
                })
            }).catch(error => {
                console.error(error);
                if (error.response?.data) {
                    window.alert(error.response.data);
                }
            });
    }


    render() {
        return <div className="hp-simple-form">
            <form onSubmit={e => this.submit(e)}>
                <h1 className="h3 mb-3 fw-normal">Edit visitor</h1>
                <label htmlFor="inputName" className="visually-hidden">Name</label>
                <input type="text" id="inputName" className="form-control" placeholder="Name" value={this.state.name} required onChange={e => this.setState({ name: e.target.value })} />
                <label htmlFor="inputEmail" className="visually-hidden">Email address</label>
                <input type="email" id="inputEmail" className="form-control" placeholder="Email address" value={this.state.email} onChange={e => this.setState({ email: e.target.value })} />
                <label htmlFor="inputMobile" className="visually-hidden">Mobile</label>
                <input type="text" id="inputMobile" className="form-control" placeholder="Mobile" value={this.state.mobile} onChange={e => this.setState({ mobile: e.target.value })} />
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
        axios.post('/visitors/update', {
            id: this.state.id,
            name: this.state.name,
            email: this.state.email,
            mobile: this.state.mobile
        }).then(() => {
            this.props.history.push('/visitors');
        }).catch(error => {
            console.error(error);
            if (error.response?.data) {
                window.alert(error.response.data);
            }
        });
    }
}

export default EditVisitorPage;