import { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Badge from 'react-bootstrap/Badge';

//let old_l = null;
function Products({ locale, direction, onLocaleChange }) {
    const [products, setProducts] = useState();
    
 //   if (old_l != locale && old_l != null) {
 //       alert("locale changed to " + locale); 
 //   }

 //   old_l = locale;

    useEffect(() => {
        populateProductsData(locale);
    }, [locale]);

    return products ?
        <>

            <Container>
                <Row>

                    {products.map(product =>
                        <Col key={product.id} >
                            <h2>{product.nameK}  <Badge pill bg="warning" text="dark">
                                {product.price}€
                            </Badge></h2>
                            <h4>{product.descriptionK}</h4>
                            <p>{product.fullK}</p>

                        </Col>
                    )}

                </Row>
            </Container>
        </> : null;

    async function populateProductsData(locale) {

        fetch('products/' + locale)
         .then((response) => response.json())
         .then((data) => {
            console.log(data);
            setProducts(data);
         })
         .catch((err) => {
            console.log(err.message);
         });

    }

}

export default Products;