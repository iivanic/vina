import { useEffect, useState } from 'react';
import { IntlProvider } from "react-intl";
import './App.css';
import Login from './components/login';
function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
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
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <IntlProvider>
            <div class="container">
                <nav class="navbar fixed-top navbar-light bg-light">
                    <a class="navbar-brand" href="#">Fixed top</a>
                </nav>
                <br></br>
                <br></br>
                <br></br>
                <br></br>
                <div class="row">
                    <div class="col">
                        <h1 id="tableLabel">Weather forecast</h1>
                        <p>This component demonstrates fetching data from the server.</p>
                        {contents}
                    </div>
                    <div class="col">
                        <Login />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>
                        <h1>jm kljopćj opć op</h1>


                    </div>
                </div>
            </div>
        </IntlProvider >
    );

    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    }
}

export default App;