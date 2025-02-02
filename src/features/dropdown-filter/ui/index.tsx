import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { ProductFilter } from "../../../entities/product";

type Props = {
  label: string;
  filters: ProductFilter[];
  currentValue: string;
  // eslint-disable-next-line
  onValueChange: (value: string) => void;
};

export const DropDownFilter = ({
  label,
  filters,
  currentValue,
  onValueChange,
}: Props) => {
  const handleChange = (event: SelectChangeEvent<string>) => {
    onValueChange(event.target.value);
  };

  return (
    <FormControl fullWidth>
      <InputLabel>{label}</InputLabel>
      <Select value={currentValue} label={label} onChange={handleChange}>
        {filters.map(({ id, title, param }) => (
          <MenuItem key={id} value={param}>
            {title}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};
