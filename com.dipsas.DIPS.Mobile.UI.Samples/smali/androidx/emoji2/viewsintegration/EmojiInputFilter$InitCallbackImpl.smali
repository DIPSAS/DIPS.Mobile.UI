.class Landroidx/emoji2/viewsintegration/EmojiInputFilter$InitCallbackImpl;
.super Landroidx/emoji2/text/EmojiCompat$InitCallback;
.source "EmojiInputFilter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/emoji2/viewsintegration/EmojiInputFilter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "InitCallbackImpl"
.end annotation


# instance fields
.field private final mEmojiInputFilterReference:Ljava/lang/ref/Reference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/Reference<",
            "Landroidx/emoji2/viewsintegration/EmojiInputFilter;",
            ">;"
        }
    .end annotation
.end field

.field private final mViewRef:Ljava/lang/ref/Reference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/Reference<",
            "Landroid/widget/TextView;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Landroid/widget/TextView;Landroidx/emoji2/viewsintegration/EmojiInputFilter;)V
    .locals 1
    .param p1, "textView"    # Landroid/widget/TextView;
    .param p2, "emojiInputFilter"    # Landroidx/emoji2/viewsintegration/EmojiInputFilter;

    .line 105
    invoke-direct {p0}, Landroidx/emoji2/text/EmojiCompat$InitCallback;-><init>()V

    .line 106
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Landroidx/emoji2/viewsintegration/EmojiInputFilter$InitCallbackImpl;->mViewRef:Ljava/lang/ref/Reference;

    .line 107
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Landroidx/emoji2/viewsintegration/EmojiInputFilter$InitCallbackImpl;->mEmojiInputFilterReference:Ljava/lang/ref/Reference;

    .line 108
    return-void
.end method

.method private isInputFilterCurrentlyRegisteredOnTextView(Landroid/widget/TextView;Landroid/text/InputFilter;)Z
    .locals 4
    .param p1, "textView"    # Landroid/widget/TextView;
    .param p2, "myInputFilter"    # Landroid/text/InputFilter;

    .line 132
    const/4 v0, 0x0

    if-eqz p2, :cond_4

    if-nez p1, :cond_0

    goto :goto_1

    .line 136
    :cond_0
    invoke-virtual {p1}, Landroid/widget/TextView;->getFilters()[Landroid/text/InputFilter;

    move-result-object v1

    .line 137
    .local v1, "currentFilters":[Landroid/text/InputFilter;
    if-nez v1, :cond_1

    .line 139
    return v0

    .line 141
    :cond_1
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    array-length v3, v1

    if-ge v2, v3, :cond_3

    .line 142
    aget-object v3, v1, v2

    if-ne v3, p2, :cond_2

    .line 143
    const/4 v0, 0x1

    return v0

    .line 141
    :cond_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 146
    .end local v2    # "i":I
    :cond_3
    return v0

    .line 134
    .end local v1    # "currentFilters":[Landroid/text/InputFilter;
    :cond_4
    :goto_1
    return v0
.end method


# virtual methods
.method public onInitialized()V
    .locals 6

    .line 112
    invoke-super {p0}, Landroidx/emoji2/text/EmojiCompat$InitCallback;->onInitialized()V

    .line 113
    iget-object v0, p0, Landroidx/emoji2/viewsintegration/EmojiInputFilter$InitCallbackImpl;->mViewRef:Ljava/lang/ref/Reference;

    invoke-virtual {v0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    .line 114
    .local v0, "textView":Landroid/widget/TextView;
    iget-object v1, p0, Landroidx/emoji2/viewsintegration/EmojiInputFilter$InitCallbackImpl;->mEmojiInputFilterReference:Ljava/lang/ref/Reference;

    invoke-virtual {v1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/text/InputFilter;

    .line 115
    .local v1, "myInputFilter":Landroid/text/InputFilter;
    invoke-direct {p0, v0, v1}, Landroidx/emoji2/viewsintegration/EmojiInputFilter$InitCallbackImpl;->isInputFilterCurrentlyRegisteredOnTextView(Landroid/widget/TextView;Landroid/text/InputFilter;)Z

    move-result v2

    if-nez v2, :cond_0

    return-void

    .line 116
    :cond_0
    invoke-virtual {v0}, Landroid/widget/TextView;->isAttachedToWindow()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 117
    invoke-static {}, Landroidx/emoji2/text/EmojiCompat;->get()Landroidx/emoji2/text/EmojiCompat;

    move-result-object v2

    invoke-virtual {v0}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object v3

    invoke-virtual {v2, v3}, Landroidx/emoji2/text/EmojiCompat;->process(Ljava/lang/CharSequence;)Ljava/lang/CharSequence;

    move-result-object v2

    .line 119
    .local v2, "result":Ljava/lang/CharSequence;
    invoke-static {v2}, Landroid/text/Selection;->getSelectionStart(Ljava/lang/CharSequence;)I

    move-result v3

    .line 120
    .local v3, "selectionStart":I
    invoke-static {v2}, Landroid/text/Selection;->getSelectionEnd(Ljava/lang/CharSequence;)I

    move-result v4

    .line 122
    .local v4, "selectionEnd":I
    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 124
    instance-of v5, v2, Landroid/text/Spannable;

    if-eqz v5, :cond_1

    .line 125
    move-object v5, v2

    check-cast v5, Landroid/text/Spannable;

    invoke-static {v5, v3, v4}, Landroidx/emoji2/viewsintegration/EmojiInputFilter;->updateSelection(Landroid/text/Spannable;II)V

    .line 128
    .end local v2    # "result":Ljava/lang/CharSequence;
    .end local v3    # "selectionStart":I
    .end local v4    # "selectionEnd":I
    :cond_1
    return-void
.end method
