import { useEffect, useState } from 'react';
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

import './App.css';
import VinaNavbar from './components/VinaNavbar';
import Container from 'react-bootstrap/Container';

let initLocale = "hr";
if (navigator.language.toLowerCase() === "hr-hr") {
    initLocale = "hr";
} else if (navigator.language.toLowerCase() === "de-de" || navigator.language.toLowerCase() === "de-at") {
    initLocale = "de";
} else initLocale = "en";

function loadMessages(locale) {
    switch (locale) {
        case "hr":
            return import("./lang/hr.json");
        case "en":
            return import("./lang/en.json");
        case "de":
            return import("./lang/de.json");
        default:
            return import("./lang/hr.json");
    }
}
function getDirection(locale) {
    switch (locale) {
        case "ar":
            return "rtl";
        case "en":
            return "ltr";
        case "de":
            return "ltr";
        default:
            return "ltr";
    }
}
function LocalizationWrapper() {
    const [locale, setLocale] = useState(initLocale);
    const [messages, setMessages] = useState(null);

    useEffect(() => {
        loadMessages(locale).then((data) => setMessages(data.default));
    }, [locale]);

    return messages ? (
        <IntlProvider locale={locale} messages={messages}>
            <App locale={locale} direction={getDirection(locale)} onLocaleChange={(locale) => setLocale(locale)} />
        </IntlProvider>
    ) : null;
}
export default LocalizationWrapper

function App({ locale, direction, onLocaleChange }) {

    const intl = useIntl();
    const [products, setProducts] = useState();

     useEffect(() => {
         populateProductsData();
     }, []);

    const contents = products === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {products.map(product =>
                    <tr key={product.nameK}>
                        <td>{product.nameK}</td>
                        <td>{product.descriptionK}</td>
                        <td>{product.fullK}</td>
                        <td>{product.price}€</td>
                    </tr>
                )}
            </tbody>
        </table>;
    
    return (
        <>
            <VinaNavbar locale={locale} direction={direction} onLocaleChange={onLocaleChange}   />
            <Container>
                <br></br>
                <br></br>
                <br></br>
                <br></br><br></br><br></br>
                <hr></hr>
                <select value={locale} onChange={(e) => onLocaleChange(e.target.value)}>
                        <option value="en">en</option>
                        <option value="hr">hr</option>
                        <option value="de">de</option>
                    </select>
                <FormattedMessage id="message.simple" />
                <h3>Imperative examples</h3>
                {intl.formatMessage({ id: "message.simple" })}
                <hr></hr>
                <div className="row">
                    <div className="col">
                        <h1 id="tableLabel">Products</h1>
                        <p>This component demonstrates fetching data from the server.</p>
                        {contents}
                    </div>
                </div>
            </Container>
            <h1 dir={direction} style={{ padding: 20 }} data-testid="examples">dsadsadas asdasdas</h1>

        </>
    );

    async function populateProductsData() {
        const response = await fetch('products/hr');
        if (response.ok) {
            const data = await response.json();
            setProducts(data);
        }
    }
}
