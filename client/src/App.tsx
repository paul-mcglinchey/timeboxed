import {
  Routes,
  Route
} from 'react-router-dom';

import { Login, PasswordReset, PasswordResetRequest, Signup, PrivateApp, NotificationContainer } from './components';
import { useTheme } from './hooks';

export default function App() {

  useTheme()

  return (
    <div className="font-sans antialiased text-color-paragraph">
      <NotificationContainer />
      <Routes>
        <Route path="/*" element={<PrivateApp />} />

        {/* Unprotected routes */}
        <Route path="login" element={<Login />} />
        <Route path="signup" element={<Signup />} />
        <Route path="passwordresetrequest" element={<PasswordResetRequest />} />
        <Route path="passwordreset" element={<PasswordReset />} />
      </Routes>
    </div>
  );
}