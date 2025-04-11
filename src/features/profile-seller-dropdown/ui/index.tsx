import { IconButton, Menu, MenuItem } from "@mui/material";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useState } from "react";
import {
  ProfileCommonActions,
  ProfileSellerActions,
} from "../../../shared/profile-actions";

type Props = {
  onMenuAction: (action: ProfileCommonActions | ProfileSellerActions) => void;
};

export const ProfileSellerDropdown = ({ onMenuAction }: Props) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null); // Состояние для меню
  const open = Boolean(anchorEl); // Открыто ли меню
  // Обработчик открытия меню
  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  // Обработчик закрытия меню
  const handleMenuClose = (
    action: ProfileCommonActions | ProfileSellerActions
  ) => {
    onMenuAction(action);
    setAnchorEl(null);
  };

  return (
    <div>
      <IconButton color="inherit" onClick={handleMenuOpen}>
        <AccountCircleIcon />
      </IconButton>

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
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCommonActions.PROFILE);
          }}>
          Профиль{" "}
          <span
            style={{
              backgroundColor: "#ffeb3b",
              color: "#000",
              padding: "2px 6px",
              borderRadius: "4px",
              marginLeft: "4px",
            }}>
            Продавца
          </span>
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileSellerActions.MY_PRODUCTS);
          }}>
          Мои продукты
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileSellerActions.ADD_PRODUCT);
          }}>
          Добавить продукт
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCommonActions.SETTINGS);
          }}>
          Настройки
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCommonActions.LOGOUT);
          }}>
          Выйти
        </MenuItem>
      </Menu>
    </div>
  );
};
