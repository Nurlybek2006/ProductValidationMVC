(() => {
    const root = document.documentElement;
    document.getElementById('themeToggle')?.addEventListener('click', () => {
        const theme = root.dataset.theme === 'light' ? 'dark' : 'light';
        root.dataset.theme = theme;
        try { localStorage.setItem('npl-theme', theme); } catch { /* browser storage may be unavailable */ }
    });

    const sidebar = document.getElementById('sidebar');
    const scrim = document.getElementById('sidebarScrim');
    const toggleSidebar = () => {
        sidebar?.classList.toggle('open');
        scrim?.classList.toggle('visible');
    };
    document.getElementById('menuToggle')?.addEventListener('click', toggleSidebar);
    scrim?.addEventListener('click', toggleSidebar);

    if (window.jQuery) window.jQuery(() => {
        const $ = window.jQuery;
        if (!$.validator?.unobtrusive) return;
        $.validator.addMethod('notfuture', (value) => {
            if (!value) return true;
            const local = new Date();
            const today = new Date(local.getTime() - local.getTimezoneOffset() * 60000).toISOString().slice(0, 10);
            return value <= today;
        });
        $.validator.unobtrusive.adapters.addBool('notfuture');
        $('form').each((_, form) => $.validator.unobtrusive.parse(form));
    });
})();
