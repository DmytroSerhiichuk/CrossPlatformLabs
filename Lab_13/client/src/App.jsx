import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';

import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import Home from "./pages";
import Layout from "./components/layout";
import Login from './pages/account/login';
import Signup from './pages/account/signup';
import Profile from './pages/profile';
import { AuthProvider } from './components/auth/AuthProvider';
import ProtectedRoute from './components/auth/ProtectedRoute';
import Lab_1 from './pages/lablib/lab-1';
import Lab_2 from './pages/lablib/lab-2';
import Lab_3 from './pages/lablib/lab-3';

const App = () => {
  axios.defaults.withCredentials = true

  return (
    <Router>
      <AuthProvider>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path='profile' element={<ProtectedRoute><Profile/></ProtectedRoute>} />
          </Route>
          <Route path="/account" element={<Layout />}>
            <Route path='login' element={<Login />} />
            <Route path='signup' element={<Signup />} />
          </Route>
          <Route path="/lablib" element={<ProtectedRoute><Layout /></ProtectedRoute>}>
            <Route path='lab-1' element={<Lab_1 />} />
            <Route path='lab-2' element={<Lab_2 />} />
            <Route path='lab-3' element={<Lab_3 />} />
          </Route>
        </Routes>
      </AuthProvider>
    </Router>
  );
}

export default App;
