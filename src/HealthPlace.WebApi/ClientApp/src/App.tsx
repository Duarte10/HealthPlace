import * as React from 'react';
import { Route } from 'react-router';
import axios from 'axios';
import { useHistory } from "react-router-dom";
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import LoginPage from './pages/Login/LoginPage';
import VisitorsPage from './pages/Visitors/VisitorsPage';
import NewVisitorPage from './pages/Visitors/NewVisitorPage';
import EditVisitorPage from './pages/Visitors/EditVisitorPage';
import VisitorOverviewPage from './pages/Visitors/VisitorOverviewPage';
import NewUserPage from './pages/Users/NewUserPage';
import UsersPage from './pages/Users/UsersPage';
import EditUsersPage from './pages/Users/EditUserPage';
import PositiveCasesPage from './pages/PositiveCases/PositiveCasesPage';
import NewPositiveCasePage from './pages/PositiveCases/NewPositiveCasePage';

import './custom.scss'

export default (props: any) => {
    let history = useHistory();
    axios.defaults.baseURL = "https://localhost:44374/api";
    axios.interceptors.response.use(response => {
        return response;
    }, error => {
        if (error.response) {
            console.error(error.response);
        }
        if (error.response.status === 401) {
            localStorage.removeItem('token');
            history.push('/login');
        }
        return Promise.reject(error);
    });

    // Add a request interceptor
    axios.interceptors.request.use(config => {
        const token = 'Bearer ' + localStorage.getItem("token");
        config.headers.Authorization = token;
        return config;
    });

    return <>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
            <Route exact path='/login' component={LoginPage} />
            <Route exact path='/visitors' component={VisitorsPage} />
            <Route exact path='/visitors/new' component={NewVisitorPage} />
            <Route exact path='/visitors/edit/:id' component={EditVisitorPage} />
            <Route exact path='/visitors/:id/overview' component={VisitorOverviewPage} />
            <Route exact path='/users' component={UsersPage} />
            <Route exact path='/users/new' component={NewUserPage} />
            <Route exact path='/users/edit/:id' component={EditUsersPage} />
            <Route exact path='/positive-cases' component={PositiveCasesPage} />
            <Route exact path='/positive-cases/new' component={NewPositiveCasePage} />
        </Layout>
    </>;
}
