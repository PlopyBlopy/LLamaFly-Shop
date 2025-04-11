import { useEffect, useState } from "react";
import ReactMarkdown from "react-markdown";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import remarkGfm from "remark-gfm";

interface Props {
  onChange: (value: string) => void; // Принимаем только строку
  value?: string;
}

export const MarkdownEditor = ({ onChange, value }: Props) => {
  const [markdown, setMarkdown] = useState("");

  useEffect(() => {
    setMarkdown(value || "");
  }, [value]);

  const handleChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
    const newValue = event.target.value;
    setMarkdown(newValue);
    onChange(newValue); // Передаем только строку
  };

  return (
    <Box sx={{ display: "flex", gap: 3, flexDirection: "column" }}>
      <TextField
        label="Описание товара (Markdown)"
        multiline
        fullWidth
        minRows={10}
        value={markdown}
        onChange={handleChange}
        variant="outlined"
      />

      <Paper elevation={3} sx={{ p: 2 }}>
        <Typography variant="h6" gutterBottom>
          Предпросмотр:
        </Typography>
        <ReactMarkdown remarkPlugins={[remarkGfm]}>{markdown}</ReactMarkdown>
      </Paper>
    </Box>
  );
};
