import React, { useEffect, useState } from "react";

function VisitedCountries() {
  const [visitedCountries, setVisitedCountries] = useState([]);

  useEffect(() => {
    const fetchVisitedCountries = async () => {
      try {
        const response = await fetch(
          `${process.env.REACT_APP_COUNTRY_API_BASE}?code=${process.env.REACT_APP_COUNTRY_API_KEY}`
        );

        const data = await response.json();
        setVisitedCountries(data);
      } catch (error) {
        console.error("Fel vid hämtning av besökta länder:", error);
      }
    };

    fetchVisitedCountries();
  }, []);

  return (
    <>
      <div className="container">
        {visitedCountries.length > 0 && (
          <div>
            <h2>Besökta länder</h2>
            <ul>
              {visitedCountries.map((country, index) => (
                <li key={index}>{country.name}</li>
              ))}
            </ul>
          </div>
        )}
      </div>
    </>
  );
}

export default VisitedCountries;
