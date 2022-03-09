using NethereumBlog;

await Balance.ShowBalanceDemoAsync();
await GuestbookBlog.GetMessagesAsync();
await GuestbookBlog.WriteMessageAsync();
await TestEvent.GetMessagesAsync();
await GuestbookBlogEvent.WaitMessageAsync();