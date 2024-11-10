import { Genre } from '../../../core/models/genre';

export interface GenreFormData {
  genre: Genre;
  isEdit: boolean;
}
