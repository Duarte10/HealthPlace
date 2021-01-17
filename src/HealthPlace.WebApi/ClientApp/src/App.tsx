import * as React from 'react';
import { Route } from 'react-router';
import axios from 'axios';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import LoginPage from './pages/Login/LoginPage';
import VisitorsPage from './pages/Visitors/VisitorsPage';
import NewVisitorPage from './pages/Visitors/NewVisitorPage';

import './custom.css'

export default (props: any) => {
    axios.defaults.baseURL = "https://localhost:44374/api";
    axios.interceptors.response.use(response => {
        return response;
    }, error => {
        if (error.response) {
            console.error(error.response);
        }
        if (error.response.status === 401) {
            localStorage.removeItem('token');
            props.push('/login');
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
        </Layout>
    </>;
}
