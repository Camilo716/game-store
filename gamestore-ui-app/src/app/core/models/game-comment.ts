export interface GameComment {
  id: string;
  userName: string;
  body: string;
  formattedBody: string;
  type: CommentType;
  parentCommentId: string;
  childrenComments: GameComment[];
}

export enum CommentType {
  Comment = 0,
  Reply = 1,
  Quote = 2,
}
