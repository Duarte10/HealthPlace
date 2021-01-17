import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import LoginPage from './pages/Login/LoginPage';
import VisitorsPage from './pages/Visitors/VisitorsPage';
import NewVisitorPage from './pages/Visitors/NewVisitorPage';

import './custom.css'

export default () => (
    <>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
            <Route exact path='/login' component={LoginPage} />
            <Route exact path='/visitors' component={VisitorsPage} />
            <Route exact path='/visitors/new' component={NewVisitorPage} />
        </Layout>
    </>

);
