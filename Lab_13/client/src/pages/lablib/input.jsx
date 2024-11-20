const Input = ({ name, onChange }) => {
  return (
    <div className="form-group mb-3">
      <label className="form-label" for="password">{name}</label>
      <input type='text' name={name} className="form-control" id={name} onChange={onChange} />
    </div>
  );
}

export default Input;