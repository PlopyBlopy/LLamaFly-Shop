import {
  Toolbar,
  Box,
  IconButton,
  Badge,
  Menu,
  MenuItem,
  Container,
} from "@mui/material";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useState } from "react";
import { SearchBar } from "../../../../widgets/search-bar";
import { CategoryMenu } from "../../../category-menu";
import { observer } from "mobx-react-lite";
import { productStore } from "../../../../entities/product";
import styles from "./index.module.css";
import {
  CATEGORYID,
  SORTORDER,
  SORTPROP,
} from "../../../../entities/product/config/filters/default-values";

export const HeaderLayout = observer(() => {
  const {
    store: { filterParams, setFilterParams, getProductCardListAction },
  } = productStore;

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null); // Состояние для меню
  const open = Boolean(anchorEl); // Открыто ли меню

  // Обработчик открытия меню
  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  // Обработчик закрытия меню
  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleSearchChange = (value: string) => {
    setFilterParams({ search: value });
    setFilterParams({ categoryId: CATEGORYID });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    getProductCardListAction();
  };

  return (
    <Container className={styles.container}>
      <Toolbar>
        <Box className={styles.logo}>
          <img
            src="/src/assets/LlamaFly-full-ver1-logo.png"
            alt="Logo"
            className={styles.logoImage}
          />
        </Box>

        {/* улучшить компонент, по сути то же что и с searchBar */}
        <Box className={styles.categoryMenu}>
          <CategoryMenu />
        </Box>

        <Box className={styles.searchBar}>
          <SearchBar
            currentSearchValue={filterParams.search}
            onValueChange={handleSearchChange}
          />
        </Box>

        {/* Преобразовать в компонент */}
        <Box className={styles.icons}>
          <IconButton color="inherit">
            <Badge badgeContent={4} color="error">
              <ShoppingCartIcon />
            </Badge>
          </IconButton>
          <IconButton color="inherit" onClick={handleMenuOpen}>
            <AccountCircleIcon />
          </IconButton>
        </Box>
        <Menu
          anchorEl={anchorEl}
          open={open}
          onClose={handleMenuClose}
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "right",
          }}
          transformOrigin={{
            vertical: "top",
            horizontal: "right",
          }}>
          <MenuItem onClick={handleMenuClose}>Профиль</MenuItem>
          <MenuItem onClick={handleMenuClose}>Добавить продукт</MenuItem>
          <MenuItem onClick={handleMenuClose}>Сменить на покупателя</MenuItem>
          <MenuItem onClick={handleMenuClose}>Настройки</MenuItem>
          <MenuItem onClick={handleMenuClose}>Выйти</MenuItem>
        </Menu>
      </Toolbar>
    </Container>
  );
});
