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
import Footer from './components/Footer';
import Products from './components/Products';
import Terms from './components/Terms';
import Privacy from './components/Privacy';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

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

    return (
        <>
            <VinaNavbar locale={locale} direction={direction} onLocaleChange={onLocaleChange} />
            <Container id="home">
                <Row className="pt-5">
                    <Col className="mt-5 pt-5">
                        <h3>
                            <FormattedMessage id="message.welcome" />
                        </h3>
                        <p>
                            {intl.formatMessage({ id: "message.winemaking1" })}
                        </p><p>
                            {intl.formatMessage({ id: "message.winemaking2" })}
                        </p><p>
                            {intl.formatMessage({ id: "message.winemaking3" })}
                        </p>
                        <hr></hr>
                        <div className="row">
                            <div className="col">
                                <Products locale={locale} direction={direction} onLocaleChange={onLocaleChange} />
                            </div>
                        </div>
                    </Col>
                </Row>
                <Row>
                    <Col id="terms">
                        <Terms locale={locale} direction={direction} onLocaleChange={onLocaleChange} />
                    </Col>
                </Row>
                <Row>
                    <Col id="privacy">
                        <Privacy locale={locale} direction={direction} onLocaleChange={onLocaleChange} />
                    </Col>
                </Row>

            </Container >
            <Footer locale={locale} direction={direction} onLocaleChange={onLocaleChange} />

        </>
    );
}
