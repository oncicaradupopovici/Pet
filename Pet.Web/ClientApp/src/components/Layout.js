import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

// eslint-disable-next-line import/no-anonymous-default-export
export default props => (
  <div>
    <NavMenu />
    <Container>
      {props.children}
    </Container>
  </div>
);
