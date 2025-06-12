import { useState } from 'react';
import {
  useIntl,
  IntlProvider,
  FormattedMessage,
  FormattedDate,
  FormattedTime,
  FormattedRelativeTime,
  FormattedNumber,
  FormattedList,
} from "react-intl";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Modal from 'react-bootstrap/Modal';

let localeNames = {
  "hr": "Hrvatski / Croatian",
  "de": "Deutsch / German",
  "en": "English",
}
import PropTypes from 'prop-types';

function VinaNavbar({ locale, direction, onLocaleChange }) {
  const [validated, setValidated] = useState(false);
  const [show, setShow] = useState(false);
  const [value, setValue] = useState(),
    onInput = ({ target: { value } }) => setValue(value);

  const intl = useIntl();

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleSubmit = (event) => {
    event.preventDefault();
    const form = event.currentTarget;
    if (form.checkValidity() === false || new RegExp("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", "g").test(value) == false) {
      event.stopPropagation();
      setValidated(false);
      console.log("Not valid");
    }
    else {
      setValidated(true);
      console.log(value);
    }
  };

  return (
    <>
      <Navbar fixed="top" expand="lg" className="bg-warning">
        <Container>
          <Navbar.Brand href="#home">
            <img
              src="grb.png"
              width="90"
              className="d-inline-block align-top"
              alt="Logo"
            />
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link href="#terms"><FormattedMessage id="nav.terms" /></Nav.Link>
              <Nav.Link href="#privacy"><FormattedMessage id="nav.privacy" /></Nav.Link>
              <Nav.Link href="#contact"><FormattedMessage id="nav.contact" /></Nav.Link>
              <NavDropdown title={localeNames[locale]} id="basic-nav-dropdown">
                <NavDropdown.Item active={locale == "hr"} onClick={(e) => { onLocaleChange("hr") }}>{localeNames["hr"]}</NavDropdown.Item>
                <NavDropdown.Item active={locale == "de"} onClick={(e) => { onLocaleChange("de") }}>{localeNames["de"]}</NavDropdown.Item>
                <NavDropdown.Item active={locale == "en"} onClick={(e) => { onLocaleChange("en") }}>{localeNames["en"]}</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item href="#action/3.4">
                  Separated link
                </NavDropdown.Item>
              </NavDropdown>
            </Nav>
          </Navbar.Collapse>
          <Navbar.Collapse className="justify-content-end">
            <Nav.Link onClick={handleShow}><FormattedMessage id="nav.login" /></Nav.Link>
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Modal show={show} onHide={handleClose} animation={false}>
        <Modal.Header closeButton>
          <Modal.Title><FormattedMessage id="nav.login.header" /></Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form noValidate validated={validated} onSubmit={handleSubmit} id="LoginForm">
            <Form.Group className="mb-3" controlId="UserEmail">
              <Form.Label><FormattedMessage id="nav.login.email" /></Form.Label><br />
              <Form.Text >
                <FormattedMessage id="nav.login.email.desc" />
              </Form.Text>
              <InputGroup hasValidation>
                <Form.Control
                  type="email"
                  placeholder={intl.formatMessage({ id: "nav.login.email.placeholder" })}
                  autoFocus
                  required
                  onChange={onInput}
                />
                <Form.Control.Feedback type="invalid">
                  <FormattedMessage id="nav.login.email.invalid" />
                </Form.Control.Feedback>
              </InputGroup>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
          <FormattedMessage id="nav.login.cancel" />
          </Button>
          <Button variant="primary" form="LoginForm" type="submit">
          <FormattedMessage id="nav.login.submit" />
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
VinaNavbar.propTypes = {
  locale: PropTypes.string.isRequired,
  direction: PropTypes.string,
  onLocaleChange: PropTypes.func.isRequired,
};

export default VinaNavbar;
