import { useState } from 'react';
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
function VinaNavbar({ locale, direction, onLocaleChange }) {
  const [validated, setValidated] = useState(false);
  const [show, setShow] = useState(false);
  const [value, setValue] = useState(),
    onInput = ({ target: { value } }) => setValue(value);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false || new RegExp("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", "g").test(value) == false) {
      event.preventDefault();
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
              src="logo.png"
              width="90"
              className="d-inline-block align-top"
              alt="Logo"
            />
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link href="#terms">Terms</Nav.Link>
              <Nav.Link href="#privacy">Privacy</Nav.Link>
              <Nav.Link href="#contact">Contact</Nav.Link>
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
            <Nav.Link onClick={handleShow}>Prijavite se za pregled narudžbi</Nav.Link>
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Modal show={show} onHide={handleClose} animation={false}>
        <Modal.Header closeButton>
          <Modal.Title>Login to Vina-Ivanic.Hr</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form noValidate validated={validated} onSubmit={handleSubmit} id="LoginForm">
            <Form.Group className="mb-3" controlId="UserEmail">
              <Form.Label>Email address</Form.Label><br/>
              <Form.Text >
                    Na navedenu adresu poslat ćemo vam link koji vam omogućava pristup.
              </Form.Text>
              <InputGroup hasValidation>
                <Form.Control
                  type="email"
                  placeholder="name@example.com"
                  autoFocus
                  required
                  onChange={onInput}
                />
                
                <Form.Control.Feedback type="invalid">
                  Please enter your email address.
                </Form.Control.Feedback>
              </InputGroup>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Cancel
          </Button>
          <Button variant="primary" form="LoginForm" type="submit">
            Login
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}


export default VinaNavbar;