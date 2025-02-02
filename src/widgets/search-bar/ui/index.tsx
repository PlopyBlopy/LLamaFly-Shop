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

  // Синхронизация searchTerm с currentSearchValue
  useEffect(() => {
    // Проверяем, что currentSearchValue действительно изменился
    if (searchTerm !== currentSearchValue) {
      setSearchTerm(currentSearchValue);
    }
  }, [currentSearchValue]); // Зависимость от currentSearchValue

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.value);
    setSearchTerm(event.target.value);
  };

  const handleChangeValue = () => {
    onValueChange(searchTerm);
  };

  // Обработка нажатия Enter
  const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      onValueChange(searchTerm); // Вызываем onValueChange сразу
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
