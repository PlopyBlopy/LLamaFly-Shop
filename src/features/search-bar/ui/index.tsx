import { useEffect, useState } from "react";
import { TextField, IconButton } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import styles from "./index.module.css";

type Props = {
  currentSearchValue: string;
  // eslint-disable-next-line
  onValueChange: (value: string) => void;
};

export const SearchBar = ({ currentSearchValue, onValueChange }: Props) => {
  const [searchTerm, setSearchTerm] = useState(currentSearchValue);

  useEffect(() => {
    if (searchTerm !== currentSearchValue) {
      setSearchTerm(currentSearchValue);
    }
  }, [currentSearchValue]);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.value);
    setSearchTerm(event.target.value);
  };

  const handleChangeValue = () => {
    onValueChange(searchTerm);
  };

  const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      onValueChange(searchTerm);
    }
  };

  return (
    <div className={styles.searchContainer}>
      <TextField
        fullWidth
        variant="outlined"
        placeholder="Поиск..."
        className={styles.searchInput}
        value={searchTerm}
        onChange={handleChange}
        onKeyDown={handleKeyDown}
      />
      <IconButton onClick={handleChangeValue}>
        <SearchIcon className={styles.searchButton} />
      </IconButton>
    </div>
  );
};
