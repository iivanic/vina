import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

function Footer({ locale, direction, onLocaleChange }) {
    return (
        <div className="bg-footer text-white">
            <footer>
                <Container>
                    <Row>
                        <Col>
                            <br></br>
                            <h3 id="contact">Contact</h3>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <hr/>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p>
                                OPG Vinarija Ivanic <br />
                                Sokolovec 16G<br />
                                HR44320 Kutina<br />
                            </p>
                            <p>
                                <span>Tel:</span> +385 91 614 6963
                            </p>
                            <p>
                                <span>Email:</span> info@vina-ivanic.hr
                            </p>
                        </Col>
                        <Col>
                            <p >
                                <span>VAT No:</span> HR 42298712672
                            </p>
                            <p >
                                <span>IBAN:</span> HR5123400091160403327
                            </p>
                            <p >
                                <span>SWIFT:</span> PBZGHR2X, Privredna banka Zagreb d.d.
                            </p>
                            <p ><a target="_blank" href="https://maps.app.goo.gl/EJ9dYSoLi69g2VSx7">
                                Click here to find us on the map -&gt;</a>
                            </p>
                        </Col>
                        <Col>

                            <p >
                                <a target="_blank" href="https://maps.app.goo.gl/EJ9dYSoLi69g2VSx7">
                                    <img className="w-100 float-end" src="map.png" alt="Location" /></a>
                            </p>
                        </Col>

                    </Row>
                    <Row>
                        <Col>
                            <hr/>
                            <p className="float-end">© Ivanić Winery 2025.</p>
                           
                        </Col>
                    </Row>
                          
                </Container>
            </footer>
        </div>
    );
}

export default Footer;