import * as React from 'react';
import axios from 'axios';
import { RouteComponentProps } from 'react-router-dom';
import { TabContent, TabPane, Nav, NavItem, NavLink } from 'reactstrap';
import classnames from 'classnames';
import moment from 'moment';
import Flatpickr from 'react-flatpickr';
import { VisitorOverview } from '../../types/VisitorOverview';
import { Visit } from '../../types/Visit';
import { PositiveCase } from '../../types/PositiveCase';
import { Notification } from '../../types/Notification';

class VisitorOverviewPage extends React.Component<RouteComponentProps<{ id: string }>, VisitorOverview> {

    constructor(props: any) {
        super(props);
        this.state = {
            id: '',
            name: '',
            email: '',
            mobile: '',
            visits: [],
            notifications: [],
            positiveCases: [],
            activeTab: '1',
            newVisit: {
                id: '',
                visitorId: '',
                checkIn: moment().toDate(),
            },
            selectedVisit: {
                id: '',
                visitorId: '',
                checkIn: moment().toDate(),
            }
        };
    }

    componentDidMount() {
        // load user data
        axios.get('visitors/' + this.props.match.params.id + '/overview')
            .then(result => {
                this.setState({
                    id: result.data.id,
                    name: result.data.name,
                    email: result.data.email ? result.data.email : '-',
                    mobile: result.data.mobile ? result.data.mobile : '-',
                    visits: result.data.visits,
                    positiveCases: result.data.positiveCases.map((p: PositiveCase) => {
                        return {
                            ...p,
                            visitDate: moment(p.visitDate).format('DD/MM/YYYY HH:mm')
                        }
                    }),
                    notifications: result.data.notifications.map((n: Notification) => {
                        return {
                            ...n,
                            sentDate: moment(n.sentDate).format('DD/MM/YYYY HH:mm')
                        }
                    })
                });
            }).catch(error => {
                console.error(error);
                if (error.response?.data) {
                    window.alert(error.response.data);
                }
            });
    }


    /**
     * Toggles between the tabs
     * @param {string} tab The selected tab
     * @memberof VisitorOverviewPage
     */
    toggle(tab: string) {
        this.setState({ activeTab: tab });
    }

    openNewVisitModal() {
        (document.getElementById('backdrop') as any).style.display = 'block';
        (document.getElementById('newVisitModal') as any).style.display = 'block';
        (document.getElementById('newVisitModal') as any).className += 'show';
    }

    closeNewVisitModal() {
        (document.getElementById('backdrop') as any).style.display = 'none';
        (document.getElementById('newVisitModal') as any).style.display = 'none';
        (document.getElementById('newVisitModal') as any).className += (document.getElementById('newVisitModal') as any).className.replace('show', '')
    }

    saveNewVisit() {
        const { checkIn, checkOut } = this.state.newVisit;
        if (checkOut && moment(checkIn).isAfter(moment(checkOut))) {
            window.alert('Check-out date cannot be before check-in!')
            return;
        }

        axios.post('/visits/new', {
            visitorId: this.state.id,
            checkIn: this.state.newVisit.checkIn,
            checkOut: this.state.newVisit.checkOut
        }).then(() => window.location.reload());
    }

    openEditVisitModal(selectedVisit: Visit) {
        this.setState({ selectedVisit });
        (document.getElementById('backdrop') as any).style.display = 'block';
        (document.getElementById('editVisitModal') as any).style.display = 'block';
        (document.getElementById('editVisitModal') as any).className += 'show';
    }

    closeEditVisitModal() {
        (document.getElementById('backdrop') as any).style.display = 'none';
        (document.getElementById('editVisitModal') as any).style.display = 'none';
        (document.getElementById('editVisitModal') as any).className += (document.getElementById('editVisitModal') as any).className.replace('show', '')
    }

    updateVisit() {
        const { checkIn, checkOut } = this.state.selectedVisit;
        if (checkOut && moment(checkIn).isAfter(moment(checkOut))) {
            window.alert('Check-out date cannot be before check-in!')
            return;
        }
        axios.post('/visits/update', this.state.selectedVisit).then(() => window.location.reload());
    }

    openDeleteVisitModal(selectedVisit: Visit) {
        this.setState({ selectedVisit });
        (document.getElementById('backdrop') as any).style.display = 'block';
        (document.getElementById('deleteVisitModal') as any).style.display = 'block';
        (document.getElementById('deleteVisitModal') as any).className += 'show';
    }

    closeDeleteVisitModal() {
        (document.getElementById('backdrop') as any).style.display = 'none';
        (document.getElementById('deleteVisitModal') as any).style.display = 'none';
        (document.getElementById('deleteVisitModal') as any).className += (document.getElementById('deleteVisitModal') as any).className.replace('show', '')
    }

    deleteVisit() {
        axios.delete('/visits/' + this.state.selectedVisit.id).then(() => window.location.reload());
    }


    render() {
        let activeTab = this.state.activeTab;
        return <>
            <Nav tabs>
                <NavItem>
                    <NavLink
                        className={classnames({ active: activeTab === '1' }, 'pointer')}
                        onClick={() => { this.toggle('1'); }}
                    >
                        Visitor
                     </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink
                        className={classnames({ active: activeTab === '2' }, 'pointer')}
                        onClick={() => { this.toggle('2'); }}>
                        Visits
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink
                        className={classnames({ active: activeTab === '3' }, 'pointer')}
                        onClick={() => { this.toggle('3'); }}>
                        Positive Cases
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink
                        className={classnames({ active: activeTab === '4' }, 'pointer')}
                        onClick={() => { this.toggle('4'); }}>
                        Notifications
                    </NavLink>
                </NavItem>
            </Nav>
            <TabContent activeTab={activeTab}>
                <TabPane tabId="1" className='p-4'>
                    <div className="d-flex w-100 flex-column">
                        <div className="mb-1 d-flex w-100 flex-column">
                            <b>Name</b>
                            {this.state.name}
                        </div>
                        <div className="mb-1 d-flex w-100 flex-column">
                            <b>Email</b>
                            {this.state.email}
                        </div>
                        <div className="mb-1 d-flex w-100 flex-column">
                            <b>Mobile</b>
                            {this.state.mobile}
                        </div>
                    </div>
                </TabPane>
                <TabPane tabId="2" className='p-4'>
                    <button className='btn btn-primary' style={{ marginBottom: '15px' }} onClick={() => this.openNewVisitModal()}>New</button>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Check-in</th>
                                <th>Check-out</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.visits.map((v: Visit) => {
                                    return <tr key={v.id}>
                                        <td>{moment(v.checkIn).format('DD/MM/YYYY HH:mm')}</td>
                                        <td>{v.checkOut ? moment(v.checkOut).format('DD/MM/YYYY HH:mm') : '-'}</td>
                                        <td>
                                            <button className="btn btn-secondary" style={{ marginRight: '5px' }} onClick={() => this.openEditVisitModal(v)}>edit</button>
                                            <button className="btn btn-secondary" onClick={() => this.openDeleteVisitModal(v)}>delete</button>
                                        </td>
                                    </tr>
                                })
                            }
                        </tbody>
                    </table>
                </TabPane>
                <TabPane tabId="3" className='p-4'>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Visit date</th>
                                <th>All users notified</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.positiveCases.map((p: PositiveCase) => {
                                    return <tr key={p.id}>
                                        <td>{p.visitDate}</td>
                                        <td>{p.allUsersNotified ? 'Yes' : 'No'}</td>
                                    </tr>
                                })
                            }
                        </tbody>
                    </table>
                </TabPane>
                <TabPane tabId="4" className='p-4'>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Case</th>
                                <th>Sent on</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.notifications.map((n: Notification) => {
                                    return <tr key={n.id}>
                                        <td><a href={'/positive-cases/' + n.positiveCaseId + '/overview'}>Case</a></td>
                                        <td>{n.sentDate}</td>
                                    </tr>
                                })
                            }
                        </tbody>
                    </table>
                </TabPane>
            </TabContent>
            <div id='newVisitModal' className='modal fade' tabIndex={-1} role='dialog'>
                <div className='modal-dialog' role='document'>
                    <div className='modal-content'>
                        <div className='modal-header'>
                            <h5 className='modal-title'>New visit</h5>
                            <button type='button' className='close' data-dismiss='modal' aria-label='Close' onClick={() => this.closeNewVisitModal()}>
                                <span aria-hidden='true'>&times;</span>
                            </button>
                        </div>
                        <div className='modal-body'>
                            <label className="visually-hidden">Check-in</label>
                            <Flatpickr
                                className='form-control'
                                data-enable-time
                                value={this.state.newVisit.checkIn}
                                options={{ time_24hr: true }}
                                onChange={(date: any) => this.setState({ newVisit: { ...this.state.newVisit, checkIn: date[0] } })}
                            />
                        </div>
                        <div className='modal-body'>
                            <label className="visually-hidden">Check-out</label>
                            <Flatpickr
                                className='form-control'
                                data-enable-time
                                value={this.state.newVisit.checkOut}
                                options={{ time_24hr: true }}
                                onChange={(date: any) => this.setState({ newVisit: { ...this.state.newVisit, checkOut: date[0] } })}
                            />
                        </div>
                        <div className='modal-footer'>
                            <button type='button' className='btn btn-primary'
                                onClick={() => this.saveNewVisit()}>
                                Save
                            </button>
                            <button type='button' className='btn btn-secondary'
                                onClick={() => this.closeNewVisitModal()}>
                                Cancel
                        </button>
                        </div>
                    </div>
                </div>
            </div>
            <div className='modal-backdrop fade show' id='backdrop' style={{ display: 'none' }}></div>
            <div id='editVisitModal' className='modal fade' tabIndex={-1} role='dialog'>
                <div className='modal-dialog' role='document'>
                    <div className='modal-content'>
                        <div className='modal-header'>
                            <h5 className='modal-title'>Edit visit</h5>
                            <button type='button' className='close' data-dismiss='modal' aria-label='Close' onClick={() => this.closeEditVisitModal()}>
                                <span aria-hidden='true'>&times;</span>
                            </button>
                        </div>
                        <div className='modal-body'>
                            <label className="visually-hidden">Check-in</label>
                            <Flatpickr
                                className='form-control'
                                data-enable-time
                                value={this.state.selectedVisit.checkIn}
                                options={{ time_24hr: true }}
                                onChange={(date: any) => this.setState({ selectedVisit: { ...this.state.selectedVisit, checkIn: date[0] } })}
                            />
                        </div>
                        <div className='modal-body'>
                            <label className="visually-hidden">Check-out</label>
                            <Flatpickr
                                className='form-control'
                                data-enable-time
                                value={this.state.selectedVisit.checkOut}
                                options={{ time_24hr: true }}
                                onChange={(date: any) => this.setState({ selectedVisit: { ...this.state.selectedVisit, checkOut: date[0] } })}
                            />
                        </div>
                        <div className='modal-footer'>
                            <button type='button' className='btn btn-primary'
                                onClick={() => this.updateVisit()}>
                                Save
                            </button>
                            <button type='button' className='btn btn-secondary'
                                onClick={() => this.closeEditVisitModal()}>
                                Cancel
                        </button>
                        </div>
                    </div>
                </div>
            </div>
            <div id='deleteVisitModal' className='modal fade' tabIndex={-1} role='dialog'>
                <div className='modal-dialog' role='document'>
                    <div className='modal-content'>
                        <div className='modal-header'>
                            <h5 className='modal-title'>Delete visit</h5>
                            <button type='button' className='close' data-dismiss='modal' aria-label='Close' onClick={() => this.closeEditVisitModal()}>
                                <span aria-hidden='true'>&times;</span>
                            </button>
                        </div>
                        <div className='modal-body'>
                            <p>Are you sure you want to delete this visit?</p>
                        </div>
                        <div className='modal-footer'>
                            <button type='button' className='btn btn-danger'
                                onClick={() => this.deleteVisit()}>
                                Delete
                            </button>
                            <button type='button' className='btn btn-secondary'
                                onClick={() => this.closeEditVisitModal()}>
                                Cancel
                        </button>
                        </div>
                    </div>
                </div>
            </div>
            <div className='modal-backdrop fade show' id='backdrop' style={{ display: 'none' }}></div>
        </>;
    }
}

export default VisitorOverviewPage;