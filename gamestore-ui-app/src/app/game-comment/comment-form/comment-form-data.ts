import { GameComment } from '../../core/models/game-comment';

export interface CommentFormData {
  comment: GameComment;
  gameKey: string;
}
