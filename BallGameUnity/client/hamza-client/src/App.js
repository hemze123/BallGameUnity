import React, { useState, useEffect } from 'react';
import './App.css';

function App() {
  const [message, setMessage] = useState('');

  useEffect(() => {
    fetch('http://localhost:5000/api/message')
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => setMessage(data.text))
      .catch(error => {
        console.error('Error:', error);
        setMessage('Bağlantı hatası!');
      });
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <h1>{message || 'Yükleniyor...'}</h1>
      </header>
    </div>
  );
}

export default App; 