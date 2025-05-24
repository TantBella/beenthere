import React, { useState } from "react";
import "./App.css";
import VisitedCountries from "./components/VisitedCountries";

function App() {
  const [name, setName] = useState("");
  const [countryInfo, setCountryInfo] = useState(null);
  const [message, setMessage] = useState("");

  const fetchCountryData = async () => {
    if (!name) {
      setMessage("Skriv in ett land");
      return;
    }
    try {
      const response = await fetch(
        `${process.env.REACT_APP_REST_COUNTRIES_API}/${name}`
      );

      const data = await response.json();
      const country = data[0];

      setCountryInfo({
        name: country.name.common,
        capital: country.capital,
        languages: country.languages,
        // nativeName: country.nativeName,
        continents: country.continents,
        flags: country.flags.png,
      });
      setMessage("");
    } catch (err) {
      setMessage(err.message);
      setCountryInfo(null);
    }
  };

  const addVisitedCountry = async () => {
    if (!countryInfo) {
      setMessage("Hämta först landet innan du lägger till det");
      return;
    }
    try {
      const response = await fetch(
        `${process.env.REACT_APP_COUNTRY_API_BASE}/add/${name}?code=${process.env.REACT_APP_COUNTRY_API_KEY}`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(countryInfo),
        }
      );

      if (!response.ok) {
        throw new Error("Kunde inte lägga till landet");
      }
      setMessage(` ${name} har lagts till i besökta länder!`);
    } catch (error) {
      setMessage(error.message);
    }
  };

  return (
    <>
      <div className="header">
        <h1>BeenThere</h1>
        <p>
          Where have you been? What have you seen? Add your travels and let the
          world remember with you.
        </p>
      </div>
      <div className="container search">
        <h2>Sök land</h2>
        <input
          type="text"
          placeholder="Skriv in ett land"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <button onClick={fetchCountryData}>Hämta valfritt land</button>
        <button onClick={addVisitedCountry}>Lägg till i besökta länder</button>

        {message && <p style={{ marginTop: 10, color: "red" }}>{message}</p>}

        {countryInfo && (
          <div>
            <h2>{countryInfo.name}</h2>

            <img
              src={countryInfo.flags}
              alt={`Flagga för ${countryInfo.name}`}
              style={{ width: "60px", height: "auto", borderRadius: "4px" }}
            />
            <p>
              <strong>Huvudstad:</strong> {countryInfo.capital}
            </p>
            <p>
              <strong>Språk:</strong>{" "}
              {Object.values(countryInfo.languages).join(", ")}
            </p>

            {/* <p>
            <strong>Språk:</strong> {countryInfo.languages}
          </p> */}
            {/* <p>
            <strong>Eget namn:</strong> {countryInfo.nativeName}
          </p> */}
            <p>
              <strong>Världsdel:</strong> {countryInfo.continents}
            </p>
          </div>
        )}
      </div>
      <VisitedCountries />

      <div className="footer">
        <p>&copy;TBTD</p>
      </div>
    </>
  );
}

export default App;
