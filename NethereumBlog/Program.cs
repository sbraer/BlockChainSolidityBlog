using NethereumBlog;

await Balance.ShowBalanceDemoAsync();
await GuestbookBlog.GetMessagesAsync();
await GuestbookBlog.WriteMessageAsync();
await TestStruct.GetMessagesAsync();
await GuestbookBlogEvent.WaitMessageAsync();