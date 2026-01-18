
import { useState } from "react";
import GenerateVideo from "./pages/GenerateVideo";
import { VideoStatusPage } from "./pages/VideoStatusPage";
import AppLayout from "./layout/AppLayout";

export default function App() {
  const [jobId, setJobId] = useState(null);

  return (
    <AppLayout>
      {!jobId ? (
        <GenerateVideo onCreated={setJobId} />
      ) : (
        <VideoStatusPage jobId={jobId} />
      )}
    </AppLayout>
  );
}

