import axios from "axios";
import { useState } from "react";
import { useAuth } from "../../components/auth/AuthProvider";
import { Navigate, useLocation } from "react-router-dom";

const Signup = () => {
  const [formData, setFormData] = useState({});
  const [error, setError] = useState([]);

  const location = useLocation();
  const { isAuthenticated, setIsAuthenticated } = useAuth();

  const handleChange = e => {
    setFormData({ ...formData, [e.target.name]: e.target.value })
    setError([]);
  }

  const handleSubmit = async e => {
    e.preventDefault();

    try {
      const res = await axios.post('http://localhost:3001/api/account/signup', { ...formData });
      console.log(res);
      setIsAuthenticated(true);
    }
    catch (err) {
      console.error(err);
      if (err && err.response && err.response.data && err.response.data.status === 400) {
        setError(...error, err.response.data.errors);
      }
    }
  }

  if (isAuthenticated) {
    return <Navigate to={location.state?.from || '/profile'}  />
  }
  else {
    return (
      <div className="text-center">
        <h1 className="display-4 pb-3">Sign Up</h1>
        <form method="post" className="text-start" onSubmit={handleSubmit}>
          <Input titile={"User Name"} type={'name'} name={'UserName'} onChange={handleChange} error={error} />
          <Input titile={"Full Name"} type={'name'} name={'Fullname'} onChange={handleChange} error={error} />
          <Input titile={"Phone"} type={'tel'} name={'PhoneNumber'} onChange={handleChange} error={error} />
          <Input titile={"Email"} type={'email'} name={'Email'} onChange={handleChange} error={error} />
          <Input titile={"Password"} type={'password'} name={'Password'} onChange={handleChange} error={error} />
          <Input titile={"Password Confirm"} type={'password'} name={'PasswordConfirm'} onChange={handleChange} error={error} />
          <button type="submit" className="btn btn-primary">Sign Up</button>
        </form>
      </div>
    );
  }
}

export default Signup;

const Input = ({titile, type, name, onChange, error}) => {
  return (
    <div className="form-group mb-3">
      <label className="form-label" for="password">{titile}</label>
      <input type={type} name={name} className="form-control" id={name} onChange={onChange} />
      <span className="text-danger">{error[name]?.join('. ')}</span>
    </div>
  );
}