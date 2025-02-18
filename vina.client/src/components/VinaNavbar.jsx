import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

let localeNames = {
  "hr": "Hrvatski / Croatian",
  "de": "Deutsch / German",
  "en": "English",
}
function VinaNavbar({ locale, direction, onLocaleChange }) {
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
          <Navbar.Text>
            Prijavite se za pregled narudžbi -&gt; <Button type="submit">Prijava</Button>
          </Navbar.Text>
        </Navbar.Collapse>
      </Container>
    </Navbar>

  );
}

export default VinaNavbar;