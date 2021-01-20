import * as React from 'react';
import axios from 'axios';
import { RouteComponentProps } from 'react-router-dom';
import { TabContent, TabPane, Nav, NavItem, NavLink } from 'reactstrap';
import classnames from 'classnames';
import moment from 'moment';
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
            activeTab: '1'
        };
    }

    componentDidMount() {
        // load user data
        axios.get('visitors/' + this.props.match.params.id + '/overview')
            .then(result => {
                console.log(result);
                this.setState({
                    id: result.data.id,
                    name: result.data.name,
                    email: result.data.email ? result.data.email : '-',
                    mobile: result.data.mobile ? result.data.mobile : '-',
                    visits: result.data.visits.map((v: Visit) => {
                        return {
                            ...v,
                            checkIn: moment(v.checkIn).format('DD/MM/YYYY HH:mm'),
                            checkOut: v.checkOut ? moment(v.checkOut).format('DD/MM/YYYY HH:mm') : '-'
                        }
                    }),
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

    render() {
        let activeTab = this.state.activeTab;
        return <>
            <Nav tabs>
                <NavItem>
                    <NavLink
                        className={classnames({ active: activeTab === '1' }, 'pointer')}
                        onClick={() => { this.toggle('1'); }}
                    >
                        User
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
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Check-in</th>
                                <th>Check-out</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.visits.map((v: Visit) => {
                                    return <tr key={v.id}>
                                        <td>{v.checkIn}</td>
                                        <td>{v.checkOut}</td>
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
                                        <td><a href={'/positive-case/' + n.positiveCaseId}>Case</a></td>
                                        <td>{n.sentDate}</td>
                                    </tr>
                                })
                            }
                        </tbody>
                    </table>
                </TabPane>
            </TabContent>
        </>;
    }
}

export default VisitorOverviewPage;