import { useState } from "react";
import Input from "./input";
import axios from "axios";

const BaseLab = ({ inputs, labNum }) => {
  const [formData, setFormData] = useState(inputs.reduce((acc, key) => {
    acc[key] = '';
    return acc;
  }, {}));
  const [result, setReult] = useState('');

  const handleSubmit = async e => {
    e.preventDefault();

    try {
      const res = await axios.post(`http://localhost:3001/api/lablib/lab-${labNum}`, { ...formData });
      console.log(res);
      setReult(res.data);
    }
    catch (err) {
      console.log(err);
      setReult(err.response.data);
    }
  }

  const handleChange = e => {
    setFormData({ ...formData, [e.target.name]: e.target.value })
  }

  return (
    <div className="container">
      <h3>Lab {labNum}</h3>
      <form method="post" onSubmit={handleSubmit}>
        {inputs.map(input => <Input name={input} onChange={handleChange} />)}

        <button type="submit" className="btn btn-primary">Submit</button>
      </form>

      <h6 className="mt-3">{result}</h6>
    </div>
  );
}

export default BaseLab;