import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

let localeNames={
  "hr": "Hrvatski / Croatian",
  "de": "Deutsch / German",
  "en": "English",
}
function VinaNavbar( {locale, direction, onLocaleChange} ) {
  return (
    <Navbar fixed="top" expand="lg" className="bg-body-tertiary">
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
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#link">Winery</Nav.Link>
            <NavDropdown title={localeNames[locale]} id="basic-nav-dropdown">
              <NavDropdown.Item onClick={(e)=>{onLocaleChange("hr")}}>Hrvatski / Croatian </NavDropdown.Item>
              <NavDropdown.Item onClick={(e)=>{onLocaleChange("de")}}>Deutsch / German</NavDropdown.Item>
              <NavDropdown.Item onClick={(e)=>{onLocaleChange("en")}}>English</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item href="#action/3.4">
                Separated link
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
        <Form inline="true">
          <Row className="d-flex flex-row-reverse">
            <Col xs="auto">
              <Form.Control
                type="text"
                placeholder="Enter Your Email to Login"
                className=" mr-sm-2"
              />
            </Col>
            </Row>
            <Row className="d-flex flex-row-reverse">
            <Col xs="auto">
              <Button type="submit">Login</Button>
            </Col>
          </Row>
        </Form>
      </Container>
    </Navbar>
  );
}

export default VinaNavbar;