import { CommonModule } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { CommentType, GameComment } from '../../core/models/game-comment';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialog } from '@angular/material/dialog';
import { CommentFormComponent } from '../comment-form/comment-form.component';
import { Game } from '../../core/models/game';

@Component({
  selector: 'app-comment-tree',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatMenuModule],
  templateUrl: './comment-tree.component.html',
  styleUrl: './comment-tree.component.css',
})
export class CommentTreeComponent {
  @Input() comments: GameComment[] = [];
  @Input() game: Game | undefined;
  @Input() reload: () => void = () => {};

  readonly formDialog = inject(MatDialog);

  quoteComment(comment: GameComment) {
    comment.type = CommentType.Quote;
    this.comment(comment);
  }

  replyToComment(comment: GameComment) {
    comment.type = CommentType.Reply;
    this.comment(comment);
  }

  banUser(comment: GameComment) {
    console.log('Ban user:', comment);
  }

  private comment(comment: GameComment) {
    const dialogRef = this.formDialog.open(CommentFormComponent, {
      data: {
        comment: {
          type: comment.type,
          parentCommentId: comment.id,
        },
        gameKey: this.game?.key,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log(this.reload);
      this.reload();
    });
  }
}
