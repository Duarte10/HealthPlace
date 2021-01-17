import * as React from 'react';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';


export default (props: { children?: React.ReactNode }) => {

    const [isLoggedIn, setIsLoggedIn] = useState(window.localStorage.getItem('token'));
    const location = useLocation();
    React.useEffect(() => {
        setIsLoggedIn(window.localStorage.getItem('token'));
    }, [location])

    return <React.Fragment>
        {isLoggedIn ? <NavMenu /> : null}
        <Container>
            {props.children}
        </Container>
    </React.Fragment>
}
