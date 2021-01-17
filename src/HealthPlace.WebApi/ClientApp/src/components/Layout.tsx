import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

export default (props: { children?: React.ReactNode }) => (
    <React.Fragment>
        {window.location.href.includes('login') ? null : <NavMenu />}
        <Container>
            {props.children}
        </Container>
    </React.Fragment>
);
