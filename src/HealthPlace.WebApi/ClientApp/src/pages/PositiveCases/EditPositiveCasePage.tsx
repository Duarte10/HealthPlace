import * as React from 'react';
import axios from 'axios';
import "flatpickr/dist/themes/material_blue.css";
import Flatpickr from 'react-flatpickr';
import Autocomplete from 'react-autocomplete';
import { RouteComponentProps } from 'react-router-dom';

type NewPositiveCaseState = {
    id: string;
    visitDate: Date;
    visitors: [];
    selectedVisitor: any;
};

class EditPositiveCasePage extends React.Component<RouteComponentProps<{ id: string }>, NewPositiveCaseState> {

    constructor(props: any) {
        super(props);
        this.state = {
            id: '',
            selectedVisitor: {
                name: '',
                id: ''
            },
            visitDate: new Date(),
            visitors: []
        }
    }

    componentDidMount() {
        this.loadPositiveCase(() => this.loadVisitors());
    }

    loadPositiveCase(callBack: () => any) {
        // load user data
        axios.get('positive-cases/' + this.props.match.params.id)
            .then(result => {
                this.setState({
                    id: result.data.id,
                    visitDate: result.data.visitDate,
                    selectedVisitor: {
                        id: result.data.visitorId,
                        name: result.data.visitorName
                    }
                })
                callBack();
            }).catch(error => {
                console.error(error);
                if (error.response?.data) {
                    window.alert(error.response.data);
                }
            });
    }

    loadVisitors() {
        axios.get('/visitors')
            .then(result => this.setState({
                visitors: result.data
            }));
    }

    onSelectedVisitorChanged(id: string) {
        if (!id) return;
        const visitor = this.state.visitors.find((v: any) => v.id === id);
        this.setState({
            selectedVisitor: visitor
        })
    }

    render() {
        return <div className="hp-simple-form">
            <form onSubmit={e => this.submit(e)} className='d-flex w-100 flex-column'>
                <h1 className="h3 mb-3 fw-normal">New positive case</h1>
                <label htmlFor="inputDate" className="visually-hidden">Visit Date</label>
                <Flatpickr
                    className='form-control'
                    data-enable-time
                    value={this.state.visitDate}
                    options={{ time_24hr: true }}
                    onChange={(date: any) => this.setState({ visitDate: date[0] })}
                />
                <label htmlFor="inputVisitor" className="visually-hidden">Visitor</label>
                <Autocomplete
                    getItemValue={(item) => item.id}
                    items={this.state.visitors}
                    renderItem={(item, isHighlighted) =>
                        <div key={item.id} style={{ background: isHighlighted ? 'lightgray' : 'white' }}>
                            {item.name}
                        </div>
                    }
                    value={this.state.selectedVisitor.name}
                    onChange={(e) => this.onSelectedVisitorChanged(e.target.value)}
                    onSelect={(value) => this.onSelectedVisitorChanged(value)}
                />
                <button className="w-100 btn btn-lg btn-primary" type="submit">Submit</button>
            </form>
        </div>;
    }

    submit(event: React.FormEvent<HTMLElement>) {
        event.preventDefault();
        if (!this.state || !this.state.visitDate) {
            window.alert('Visit date is required');
            return;
        }

        if (!this.state.selectedVisitor) {
            window.alert('Visitor is required!');
            return;
        }
        axios.post('/positive-cases/update', {
            id: this.state.id,
            visitDate: this.state.visitDate,
            visitorId: this.state.selectedVisitor.id
        }).then(() => {
            this.props.history.push('/positive-cases');
        }).catch(error => {
            console.error(error);
            if (error.response?.data) {
                window.alert(error.response.data);
            }
        });
    }
}

export default EditPositiveCasePage;