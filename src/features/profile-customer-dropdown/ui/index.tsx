import { IconButton, Menu, MenuItem } from "@mui/material";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useState } from "react";
import {
  ProfileCommonActions,
  ProfileCustomerActions,
} from "../../../shared/profile-actions";

type Props = {
  onMenuAction: (action: ProfileCommonActions | ProfileCustomerActions) => void;
};

export const ProfileCustomerDropdown = ({ onMenuAction }: Props) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null); // Состояние для меню
  const open = Boolean(anchorEl); // Открыто ли меню
  // Обработчик открытия меню
  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  // Обработчик закрытия меню
  const handleMenuClose = (
    action: ProfileCommonActions | ProfileCustomerActions
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
            Покупателя
          </span>
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCustomerActions.ORDER_HISTORY);
          }}>
          История заказов
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCommonActions.SWITCH_ROLE);
          }}>
          Сменить на продавца
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCommonActions.SETTINGS);
          }}>
          Настройки
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose(ProfileCommonActions.PROFILE);
          }}>
          Выйти
        </MenuItem>
      </Menu>
    </div>
  );
};
