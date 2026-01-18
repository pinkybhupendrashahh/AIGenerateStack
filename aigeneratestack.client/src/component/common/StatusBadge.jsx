import { motion } from "framer-motion";

export default function StatusBadge({ status }) {
  return (
    <motion.div
      key={status}
      initial={{ opacity: 0, y: 5 }}
      animate={{ opacity: 1, y: 0 }}
      className="text-sm font-medium text-gray-700 dark:text-gray-300"
    >
      {status}
    </motion.div>
  );
}
