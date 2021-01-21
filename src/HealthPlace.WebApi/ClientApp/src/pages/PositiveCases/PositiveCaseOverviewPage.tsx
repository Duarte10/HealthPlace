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
import { PositiveCaseOverview } from '../../types/PositiveCaseOverview';

class PositiveCaseOverviewPage extends React.Component<RouteComponentProps<{ id: string }>, PositiveCaseOverview> {

    constructor(props: any) {
        super(props);
        this.state = {
            id: '',
            activeTab: '1',
            visitorId: '',
            visitorName: '',
            allUsersNotified: false,
            visitDate: moment().toDate(),
            collidingVisits: []
        };
    }

    componentDidMount() {
        // load user data
        axios.get('positive-cases/' + this.props.match.params.id + '/overview')
            .then(result => {
                console.log(result);
                this.setState({
                    ...result.data
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

    markNotificationSent(visitorId: string) {
        axios.post('/visitor-notifications/new', {
            visitorId,
            positiveCaseId: this.state.id,
            sentDate: moment().toDate()
        }).then(() => window.location.reload());
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
                        Positive Case
                     </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink
                        className={classnames({ active: activeTab === '2' }, 'pointer')}
                        onClick={() => { this.toggle('2'); }}>
                        Colliding Visits
                    </NavLink>
                </NavItem>
            </Nav>
            <TabContent activeTab={activeTab}>
                <TabPane tabId="1" className='p-4'>
                    <div className="d-flex w-100 flex-column">
                        <div className="mb-1 d-flex w-100 flex-column">
                            <b>Visit Date</b>
                            {moment(this.state.visitDate).format('DD/MM/YYYY HH:mm')}
                        </div>
                        <div className="mb-1 d-flex w-100 flex-column">
                            <b>Visitor</b>
                            {this.state.visitorName}
                        </div>
                        <div className="mb-1 d-flex w-100 flex-column">
                            <b>All users notified</b>
                            {this.state.allUsersNotified ? 'Yes' : 'No'}
                        </div>
                    </div>
                </TabPane>
                <TabPane tabId="2" className='p-4'>
                    {/* notificationSent: false
visitDate: "2021-01-22T12:41:00+00:00"
visitId: "033256ac-6394-47ee-a677-0c03fbf4e31d"
visitorName: "Rafael" */}
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Visitor</th>
                                <th>Visit Date</th>
                                <th>Notification sent</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.collidingVisits.map((v: any) => {
                                    return <tr key={v.visitId}>
                                        <td>{v.visitorName}</td>
                                        <td>{moment(v.visitDate).format('DD/MM/YYYY HH:mm')}</td>
                                        <td>{v.notificationSent ? 'Yes' : 'No'}</td>
                                        <td>
                                            {
                                                v.notificationSent
                                                    ? ''
                                                    : <button className='btn btn-secondary' onClick={() => this.markNotificationSent(v.visitorId)}>mark notification sent</button>
                                            }
                                        </td>
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

export default PositiveCaseOverviewPage;