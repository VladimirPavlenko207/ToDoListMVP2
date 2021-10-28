using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;
using ToDoListPresenter;
using ToDoListWForms.Enums;
using ToDoListWForms.Helpers;

namespace ToDoListWForms
{

    public partial class MainForm : Form, IView
    {
        private TaskResponseModel currentTask;
        private Func<Task<bool>> CategoryUpdate;
        private Func<Task<bool>> TagUpdate;
        private Func<Task<bool>> TaskUpdate;

        private CrudMode taskMode;
        private CrudMode tagMode;
        private CrudMode categoryMode;

        private CrudMode TaskMode { set => SetTaskMode(value); }
        private CrudMode TagMode { set => SetTagMode(value); }
        private CrudMode CategoryMode { set => SetCategoryMode(value); }

        public MainForm()
        {
            InitializeComponent();
        }

        #region IView
        public TaskResponseModel CurrentTask
        {
            get => currentTask;
            set => SetCurrentTask(value);
        }

        public CategoryResponseModel CurrentCategory { get; set; }
        public TagResponseModel CurrentTag { get; set; }

        public event Action<string> CurrentTagChenged;
        public event Action<string> CurrentCategoryChenged;
        public event Action<int> CurrentTaskChenged;

        public event Func<TagRequestModel, Task<bool>> AddNewTagClick;
        public event Func<CategoryRequestModel, Task<bool>> AddNewCategoryClick;
        public event Func<TaskRequestModel, Task<bool>> AddNewTaskClick;

        public event Func<TagRequestModel, Task<bool>> EditTagClick;
        public event Func<CategoryRequestModel, Task<bool>> EditCategoryClick;
        public event Func<TaskRequestModel, Task<bool>> EditTaskClick;

        public event Func<TagResponseModel, Task<bool>> RemoveTagClick;
        public event Func<CategoryResponseModel, Task<bool>> RemoveCategoryClick;
        public event Func<TaskResponseModel, Task<bool>> RemoveTaskClick;

        public event Action TotalLoading;

        public void SetTags(string[] tagNames)
        {
            comboBoxTags.Items.Clear();
            comboBoxRightTags.Items.Clear();
            comboBoxTags.Items.Add("Все метки");
            comboBoxTags.SelectedIndex = 0;
            if (tagNames is null) return;

            comboBoxTags.Items.AddRange(tagNames);
            comboBoxRightTags.Items.AddRange(tagNames);
        }

        public void SetCategories(string[] categoryNames)
        {
            comboBoxCategorys.Items.Clear();
            comboBoxRightCat.Items.Clear();
            comboBoxCategorys.Items.Add("Все категории");
            comboBoxCategorys.SelectedIndex = 0;
            if (categoryNames is null) return;

            comboBoxCategorys.Items.AddRange(categoryNames);
            comboBoxRightCat.Items.AddRange(categoryNames);
        }

        public void SetTasks(List<TaskResponseModel> tasks)
        {
            dataGridView1.Rows.Clear();
            if (tasks is null) tasks = new();

            int count = 0;
            dataGridView1.Rows.Clear();
            foreach (var task in tasks)
            {
                dataGridView1.Rows.Add(
                    ++count,
                    TextEditor.ReduceRowToN(task.Description, 20),
                    task.Id,
                    task.Category.Name,
                    task.Priority.ToString(),
                    task.Deadline.ToString(),
                    task.IsComplete
                    );
            }
        }

        public void Exit()
        {
            Application.Exit();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            radioButtonViewTask.Checked = true;
            comboBoxRightState.Items.AddRange(new string[] { "Актуальна", "Завершена" });
            comboBoxRightPrior.Items.AddRange(Enum.GetNames(typeof(ThreadPriority)));

            TotalLoading?.Invoke();
            CategoryMode = CrudMode.View;
            TagMode = CrudMode.View;
        }

        #region Categories hendlers

        private void comboBoxCategorys_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            var categoryName = cb.SelectedIndex > 0 ? cb.Text : string.Empty;
            CurrentCategoryChenged?.Invoke(categoryName);
            buttonRemoveCategory.Enabled = buttonEditCategory.Enabled = cb.SelectedIndex > 0;
        }

        private async void textBoxCategory_KeyDown(object sender, KeyEventArgs e)
        {
            SetCategoryUpdate();
            if (e.KeyCode is Keys.Enter)
            {
                var result = await CategoryUpdate?.Invoke();
                CategoryMode = CrudMode.View;
            }
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            CategoryMode = categoryMode == CrudMode.Add ? CrudMode.View : CrudMode.Add;
        }

        private void buttonRemoveCategory_Click(object sender, EventArgs e)
        {
            if (CurrentCategory is null) return;
            RemoveCategoryClick?.Invoke(CurrentCategory);
        }

        private void buttonEditCategory_Click(object sender, EventArgs e)
        {
            CategoryMode = categoryMode == CrudMode.Edit ? CrudMode.View : CrudMode.Edit;
        }

        #endregion

        #region Tasks hendlers
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count == 0)
            {
                CurrentTask = null;
            }
            else
            {
                var taskId = Convert.ToInt32(dgv.SelectedRows[0].Cells[2].Value);
                CurrentTaskChenged?.Invoke(taskId);
            }
            ShowCurrentTask(CurrentTask);
        }

        private void ClearCurrentTaskView()
        {
            textBoxRightTask.Clear();
            comboBoxRightCat.SelectedIndex = -1;
            comboBoxRightPrior.SelectedIndex = -1;
            dateTimePickerRightDeadline.Value = DateTime.Now;
            comboBoxRightState.SelectedIndex = -1;
            listBoxRightTags.Items?.Clear();
            comboBoxRightTags.SelectedIndex = -1;
        }

        private void ShowCurrentTask(TaskResponseModel currentTask)
        {
            ClearCurrentTaskView();
            if (currentTask is null) return;

            textBoxRightTask.Text = currentTask.Description;
            comboBoxRightCat.SelectedItem = currentTask.Category.Name;
            comboBoxRightPrior.SelectedItem = currentTask.Priority.ToString();
            dateTimePickerRightDeadline.Value = Convert.ToDateTime(currentTask.Deadline);
            comboBoxRightState.SelectedIndex = Convert.ToInt32(currentTask.IsComplete);

            if (currentTask.Tags.Count > 0)
                listBoxRightTags.Items.AddRange(currentTask.Tags.Select(t => t.Name).ToArray());
        }
        #endregion

        #region Current task hendlers
        private void textBoxRightTask_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRightCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRightPrior_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRightState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerRightDeadline_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonRightAddTag_Click(object sender, EventArgs e)
        {
            comboBoxRightTags.Visible = !comboBoxRightTags.Visible;
        }

        private void buttonRightRemoveTag_Click(object sender, EventArgs e)
        {
            var lbrt = listBoxRightTags;
            var item = lbrt.SelectedItem;
            lbrt.Items.Remove(item);
        }

        private void comboBoxRightTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            var tagName = comboBox.Text;
            string[] taskTags = listBoxRightTags.Items.OfType<string>().ToArray();

            if (!TextEditor.IsWordInArray(taskTags, tagName))
                listBoxRightTags.Items.Add(tagName);
            comboBox.Visible = false;
        }

        private void listBoxRightTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRightRemoveTag.Enabled = listBoxRightTags.SelectedIndex >= 0;
        }

        private async void buttonSaveTask_Click(object sender, EventArgs e)
        {
            if (CurrentTask is not null)
            {
                SetTaskUpdate();
                _ = await TaskUpdate?.Invoke();
                radioButtonViewTask.Checked = true;
            }
        }

        private void buttonRemoveTask_Click(object sender, EventArgs e)
        {
            RemoveTaskClick?.Invoke(currentTask);
        }

        private void radioButtonsTask_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (!rb.Checked) return;

            switch (rb.Tag)
            {
                case "view":
                    TaskMode = CrudMode.View;
                    break;
                case "add":
                    TaskMode = CrudMode.Add;
                    break;
                case "edit":
                    TaskMode = CrudMode.Edit;
                    break;
            }
        }
        #endregion

        #region Tags hendlers
        private void comboBoxTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            var tagName = cb.SelectedIndex > 0 ? cb.Text : string.Empty;
            CurrentTagChenged?.Invoke(tagName);
            buttonRemoveTag.Enabled = buttonEditTag.Enabled = cb.SelectedIndex > 0;
        }

        private void buttonAddTag_Click(object sender, EventArgs e)
        {
            TagMode = tagMode == CrudMode.Add ? CrudMode.View : CrudMode.Add;
            if (tagMode != CrudMode.Add) return;

            var newTag = new TagRequestModel { Name = textBoxTag.Text };
            TagUpdate = EventInvoker.InvokeAddNewTagClick(AddNewTagClick, newTag);
        }

        private void buttonRemoveTag_Click(object sender, EventArgs e)
        {
            RemoveTagClick?.Invoke(CurrentTag);
        }

        private void buttonEditTag_Click(object sender, EventArgs e)
        {
            TagMode = tagMode == CrudMode.Edit ? CrudMode.View : CrudMode.Edit;
            if (tagMode != CrudMode.Edit) return;

            var newTag = new TagRequestModel { Name = comboBoxTags.Text, NewName = textBoxTag.Text };
            TagUpdate = EventInvoker.InvokeEditTagClick(EditTagClick, newTag);
        }

        private async void textBoxTag_KeyDown(object sender, KeyEventArgs e)
        {
            SetTagUpdate();
            if (e.KeyCode is Keys.Enter)
            {
                var result = await TagUpdate?.Invoke();
                TagMode = CrudMode.View;
            }
        }

        #endregion

        private void buttonUpDate_Click(object sender, EventArgs e)
        {
            TotalLoading?.Invoke();
        }

        #region Tasks methods
        private void SetTaskMode(CrudMode value)
        {
            taskMode = value;
            switch (taskMode)
            {
                case CrudMode.View:
                    SetTaskControlsState(false, true, false);
                    ShowCurrentTask(CurrentTask);
                    break;
                case CrudMode.Add:
                    SetTaskControlsState(true, false, false);
                    ShowCurrentTask(null);
                    break;
                case CrudMode.Edit:
                    SetTaskControlsState(true, true, true);
                    ShowCurrentTask(CurrentTask);
                    break;
            }
        }

        private void SetTaskControlsState(bool v1, bool v2, bool v3)
        {
            textBoxRightTask.ReadOnly = !v1;
            comboBoxRightCat.Enabled = v1;
            comboBoxRightPrior.Enabled = v1;
            dateTimePickerRightDeadline.Enabled = v1;
            buttonRightAddTag.Enabled = v1;
            comboBoxRightTags.Visible = false;
            buttonRightRemoveTag.Enabled = false;
            listBoxRightTags.Enabled = v1;

            buttonSaveTask.Enabled = v1;
            buttonDeleteTask.Enabled = v2;
            comboBoxRightState.Enabled = v3;
        }

        private TaskRequestModel GetNewTask()
        {
            TaskRequestModel task = new()
            {
                Description = textBoxRightTask.Text,
                Priority = comboBoxRightPrior.SelectedIndex,
                Deadline = dateTimePickerRightDeadline.Value.ToString(),
                CategoryName = comboBoxRightCat.Text,
                IsComplete = Convert.ToBoolean(comboBoxRightState.SelectedIndex),
                TagsNames = listBoxRightTags.Items.OfType<string>().ToList()
            };
            if (taskMode == CrudMode.Edit) task.Id = currentTask.Id;
            return task;
        }

        private void SetTaskUpdate()
        {
            switch (taskMode)
            {
                case CrudMode.Add:
                    TaskUpdate = EventInvoker.InvokeAddNewTaskClick(AddNewTaskClick, GetNewTask());
                    break;
                case CrudMode.Edit:
                    TaskUpdate = EventInvoker.InvokeEditTaskClick(EditTaskClick, GetNewTask());
                    break;
            }
        }

        private void SetCurrentTask(TaskResponseModel value)
        {
            currentTask = value;
            if (currentTask is null)
            {
                radioButtonAddTask.Checked = true;
                RadioButtonsConfig(false, false);
                TaskMode = CrudMode.Add;
            }
            else
            {
                radioButtonViewTask.Checked = true;
                RadioButtonsConfig(true, true);
            }
        }

        private void RadioButtonsConfig(bool v1, bool v2)
        {
            radioButtonEditTask.Enabled = v1;
            radioButtonViewTask.Enabled = v2;
        }
        #endregion

        #region Tags methods
        private void SetTagMode(CrudMode value)
        {
            tagMode = value;
            textBoxTag.Clear();
            switch (tagMode)
            {
                case CrudMode.View:
                    bool flag = comboBoxTags.SelectedIndex > 0;
                    SetTagControlsState(false, true, flag, flag, true);
                    break;
                case CrudMode.Add:
                    SetTagControlsState(true, true, false, false, false);
                    break;
                case CrudMode.Edit:
                    textBoxTag.Text = comboBoxTags.Text;
                    SetTagControlsState(true, false, false, true, false);
                    break;
            }
        }

        private void SetTagControlsState(bool v1, bool v2, bool v3, bool v4, bool v5)
        {
            textBoxTag.Visible = v1;
            buttonAddTag.Enabled = v2;
            buttonRemoveTag.Enabled = v3;
            buttonEditTag.Enabled = v4;
            comboBoxTags.Enabled = v5;
        }

        private void SetTagUpdate()
        {
            var newTag = new TagRequestModel();
            switch (tagMode)
            {
                case CrudMode.Add:
                    newTag.Name = textBoxTag.Text;
                    TagUpdate = EventInvoker.InvokeAddNewTagClick(AddNewTagClick, newTag);
                    break;
                case CrudMode.Edit:
                    newTag.Name = comboBoxTags.Text;
                    newTag.NewName = textBoxTag.Text;
                    TagUpdate = EventInvoker.InvokeEditTagClick(EditTagClick, newTag);
                    break;
            }
        }
        #endregion

        #region Categories methods
        private void SetCategoryMode(CrudMode value)
        {
            categoryMode = value;
            textBoxCategory.Clear();
            switch (categoryMode)
            {
                case CrudMode.View:
                    bool flag = comboBoxCategorys.SelectedIndex > 0;
                    SetCategoryControlsState(false, true, flag, flag, true);
                    break;
                case CrudMode.Add:
                    SetCategoryControlsState(true, true, false, false, false);
                    break;
                case CrudMode.Edit:
                    textBoxCategory.Text = comboBoxCategorys.Text;
                    SetCategoryControlsState(true, false, false, true, false);
                    break;
            }
        }

        private void SetCategoryControlsState(bool v1, bool v2, bool v3, bool v4, bool v5)
        {
            textBoxCategory.Visible = v1;
            buttonAddCategory.Enabled = v2;
            buttonRemoveCategory.Enabled = v3;
            buttonEditCategory.Enabled = v4;
            comboBoxCategorys.Enabled = v5;
        }

        private void SetCategoryUpdate()
        {
            var newCategory = new CategoryRequestModel();
            switch (categoryMode)
            {
                case CrudMode.Add:
                    newCategory.Name = textBoxCategory.Text;
                    CategoryUpdate = EventInvoker.InvokeAddNewCategoryClick(AddNewCategoryClick, newCategory);
                    break;
                case CrudMode.Edit:
                    newCategory.Name = comboBoxCategorys.Text;
                    newCategory.NewName = textBoxCategory.Text;
                    CategoryUpdate = EventInvoker.InvokeEditCategoryClick(EditCategoryClick, newCategory);
                    break;
            }
        }

        #endregion

    }
}