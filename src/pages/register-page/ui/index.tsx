import {
  Card,
  CardActions,
  CardContent,
  Typography,
  Button,
} from "@mui/material";
import { useAppNavigate } from "../../../shared/routing/routes";

export const RegisterPage = () => {
  const { goToRegisterSeller, goToRegisterCustomer } = useAppNavigate();

  const RegisterHandler = (isSeller: boolean) => {
    if (isSeller) {
      goToRegisterSeller();
    } else if (!isSeller) {
      goToRegisterCustomer();
    }
  };

  return (
    <>
      <Card sx={{ minWidth: 275 }}>
        <CardContent>
          <Typography variant="h4">Вы регистрируетесь как...</Typography>
        </CardContent>
        <CardActions>
          <Button size="large" onClick={() => RegisterHandler(true)}>
            Продавец
          </Button>
          <Button size="large" onClick={() => RegisterHandler(false)}>
            Покупатель
          </Button>
        </CardActions>
      </Card>
    </>
  );
};
