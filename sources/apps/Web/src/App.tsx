import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import Home from './pages/Home';
import History from './pages/History';
import './App.css';

function App() {
  return (
    <BrowserRouter>
        <nav>
          <Link to="/">Home</Link> |{" "}
          <Link to="/history">History</Link>
        </nav>

        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/history" element={<History />} />
        </Routes>
    </BrowserRouter>
  );
}

export default App;