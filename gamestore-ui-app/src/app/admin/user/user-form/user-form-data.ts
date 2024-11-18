import { User } from '../../../core/models/user';

export interface UserFormData {
  user: User;
  isEdit: boolean;
}
