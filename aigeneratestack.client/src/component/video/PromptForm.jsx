import { useState } from "react";
import Input from "../common/Input";
import Button from "../common/Button";
import ErrorAlert from "../common/ErrorAlert";
export default function PromptForm({ onSubmit, loading }) {
  const [script, setScript] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = () => {
    if (!script.trim()) {
      setError("Please enter a script to generate video.");
      return;
    }
    setError("");
    onSubmit(script);
  };

  return (
    <div className="space-y-4">
      <Input
        label="Video Script"
        rows={4}
        placeholder="Explain async/await in .NET with a simple example..."
        value={script}
        onChange={(e) => setScript(e.target.value)}
      />

      <ErrorAlert message={error} />

      <Button loading={loading} onClick={handleSubmit}>
        Generate Video
      </Button>
    </div>
  );
}
