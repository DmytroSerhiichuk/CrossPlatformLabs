import { useState } from "react";
import { Navigate, useLocation } from "react-router-dom";
import axios from 'axios'
import { useAuth } from "../../components/auth/AuthProvider";

const Login = () => {
  const [formData, setFormData] = useState({});
  const [error, setError] = useState("");

  const location = useLocation();
  const { isAuthenticated, setIsAuthenticated, isLoading } = useAuth();

  const handleChange = e => {
    setFormData({...formData, [e.target.name]: e.target.value})
  }

  const handleSubmit = async e => {
    e.preventDefault();

    try {
      const res = await axios.post('http://localhost:3001/api/account/login', {...formData});
      console.log(res);
      setIsAuthenticated(true);
    }
    catch (err) {
      console.error(err);
      if (err && err.response && err.response.data && err.response.data.status === 403) {
        setError("Invalid email or password");
      }
    }
  }

  if (isAuthenticated) {
    return <Navigate to={location.state?.from || '/profile'}  />
  }
  else if (!isLoading) {
    return (
      <div className="text-center">
        <h1 className="display-4 pb-3">Login</h1>
        <form method="post" className="text-start" onSubmit={handleSubmit}>
          <span className="text-danger">{error}</span>
          <div className="form-group mb-3">
            <label className="form-label" for="email">Email</label>
            <input type="email" name="email" className="form-control" id="email" onChange={handleChange} />
          </div>
          <div className="form-group mb-3">
            <label className="form-label" for="password">Password</label>
            <input type="password" name="password" className="form-control" id="password" onChange={handleChange} />
          </div>
          <button type="submit" className="btn btn-primary">Login</button>
        </form>
      </div>
    );
  }
}

export default Login;