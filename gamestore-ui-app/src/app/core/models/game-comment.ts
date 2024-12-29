export interface GameComment {
  id: string;
  userName: string;
  body: string;
  formattedBody: string;
  type: CommentType;
  parentId: string;
  childrenComments: GameComment[];
}

export enum CommentType {
  Comment,
  Reply,
  Quote,
}
