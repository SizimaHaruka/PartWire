/*
 PartWire initial schema draft for SQL Server.
 This draft is the pre-implementation baseline for the first EF Core model.
 SQLite support is expected to use the same logical model with provider-specific adjustments.
*/

CREATE TABLE construction_projects (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    construction_no VARCHAR(50) NOT NULL,
    construction_name NVARCHAR(200) NOT NULL,
    status VARCHAR(30) NOT NULL,
    start_date DATE NULL,
    end_date DATE NULL,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_construction_projects_construction_no UNIQUE (construction_no)
);

CREATE TABLE departments (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    department_code VARCHAR(50) NOT NULL,
    department_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_departments_is_active DEFAULT 1,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_departments_department_code UNIQUE (department_code)
);

CREATE TABLE approval_ranks (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    rank_name NVARCHAR(100) NOT NULL,
    description NVARCHAR(500) NULL,
    display_order INT NOT NULL,
    CONSTRAINT uq_approval_ranks_rank_name UNIQUE (rank_name)
);

CREATE TABLE amount_ranks (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    rank_name NVARCHAR(100) NOT NULL,
    min_amount DECIMAL(18,2) NOT NULL,
    max_amount DECIMAL(18,2) NULL,
    display_order INT NOT NULL,
    CONSTRAINT uq_amount_ranks_rank_name UNIQUE (rank_name)
);

CREATE TABLE manufacturers (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    manufacturer_code VARCHAR(50) NULL,
    manufacturer_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_manufacturers_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_manufacturers_manufacturer_code UNIQUE (manufacturer_code),
    CONSTRAINT uq_manufacturers_manufacturer_name UNIQUE (manufacturer_name)
);

CREATE TABLE materials (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    material_code VARCHAR(50) NULL,
    material_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_materials_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    CONSTRAINT uq_materials_material_code UNIQUE (material_code),
    CONSTRAINT uq_materials_material_name UNIQUE (material_name)
);

CREATE TABLE shapes (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    shape_code VARCHAR(50) NULL,
    shape_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_shapes_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    CONSTRAINT uq_shapes_shape_code UNIQUE (shape_code),
    CONSTRAINT uq_shapes_shape_name UNIQUE (shape_name)
);

CREATE TABLE processing_methods (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    processing_method_code VARCHAR(50) NULL,
    processing_method_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_processing_methods_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    CONSTRAINT uq_processing_methods_code UNIQUE (processing_method_code),
    CONSTRAINT uq_processing_methods_name UNIQUE (processing_method_name)
);

CREATE TABLE sizes (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    size_code VARCHAR(50) NULL,
    size_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_sizes_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    CONSTRAINT uq_sizes_code UNIQUE (size_code),
    CONSTRAINT uq_sizes_name UNIQUE (size_name)
);

CREATE TABLE surface_treatments (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    surface_treatment_code VARCHAR(50) NULL,
    surface_treatment_name NVARCHAR(200) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT df_surface_treatments_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    CONSTRAINT uq_surface_treatments_code UNIQUE (surface_treatment_code),
    CONSTRAINT uq_surface_treatments_name UNIQUE (surface_treatment_name)
);

CREATE TABLE users (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    login_id VARCHAR(100) NOT NULL,
    user_name NVARCHAR(200) NOT NULL,
    auth_type VARCHAR(20) NOT NULL,
    password_hash VARCHAR(500) NULL,
    department_id BIGINT NULL,
    role_code VARCHAR(50) NOT NULL,
    approval_rank_id BIGINT NULL,
    ad_key VARCHAR(255) NULL,
    is_active BIT NOT NULL CONSTRAINT df_users_is_active DEFAULT 1,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_users_login_id UNIQUE (login_id),
    CONSTRAINT fk_users_department FOREIGN KEY (department_id) REFERENCES departments(id),
    CONSTRAINT fk_users_approval_rank FOREIGN KEY (approval_rank_id) REFERENCES approval_ranks(id)
);

CREATE TABLE ad_group_links (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    group_name VARCHAR(255) NOT NULL,
    purpose_type VARCHAR(50) NOT NULL,
    department_id BIGINT NULL,
    role_code VARCHAR(50) NULL,
    approval_rank_id BIGINT NULL,
    is_active BIT NOT NULL CONSTRAINT df_ad_group_links_is_active DEFAULT 1,
    CONSTRAINT uq_ad_group_links_group_name UNIQUE (group_name),
    CONSTRAINT fk_ad_group_links_department FOREIGN KEY (department_id) REFERENCES departments(id),
    CONSTRAINT fk_ad_group_links_approval_rank FOREIGN KEY (approval_rank_id) REFERENCES approval_ranks(id)
);

CREATE TABLE system_settings (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    auth_type VARCHAR(20) NOT NULL,
    ad_domain VARCHAR(255) NULL,
    ad_group_lookup_enabled BIT NOT NULL CONSTRAINT df_system_settings_group_lookup DEFAULT 0,
    quotation_forwarding_enabled BIT NOT NULL CONSTRAINT df_system_settings_quote_forward DEFAULT 0,
    password_policy_json NVARCHAR(MAX) NULL,
    updated_at DATETIME2 NOT NULL
);

CREATE TABLE business_partners (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    partner_code VARCHAR(50) NOT NULL,
    partner_name NVARCHAR(200) NOT NULL,
    contact_name NVARCHAR(100) NULL,
    email VARCHAR(255) NULL,
    phone VARCHAR(50) NULL,
    is_supplier BIT NOT NULL CONSTRAINT df_business_partners_is_supplier DEFAULT 0,
    is_processor BIT NOT NULL CONSTRAINT df_business_partners_is_processor DEFAULT 0,
    requires_invoice_management BIT NOT NULL CONSTRAINT df_business_partners_requires_invoice DEFAULT 0,
    receives_invoice_here BIT NOT NULL CONSTRAINT df_business_partners_receives_invoice DEFAULT 0,
    is_active BIT NOT NULL CONSTRAINT df_business_partners_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_business_partners_partner_code UNIQUE (partner_code)
);

CREATE TABLE purchase_parts (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    part_code VARCHAR(50) NULL,
    part_name NVARCHAR(200) NULL,
    manufacturer_id BIGINT NOT NULL,
    model_no NVARCHAR(200) NOT NULL,
    unit NVARCHAR(50) NULL,
    default_business_partner_id BIGINT NULL,
    is_active BIT NOT NULL CONSTRAINT df_purchase_parts_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_purchase_parts_part_code UNIQUE (part_code),
    CONSTRAINT fk_purchase_parts_manufacturer FOREIGN KEY (manufacturer_id) REFERENCES manufacturers(id),
    CONSTRAINT fk_purchase_parts_default_partner FOREIGN KEY (default_business_partner_id) REFERENCES business_partners(id)
);

CREATE TABLE manufactured_parts (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    drawing_no NVARCHAR(100) NOT NULL,
    item_name NVARCHAR(200) NOT NULL,
    material_id BIGINT NULL,
    shape_id BIGINT NULL,
    processing_method_id BIGINT NULL,
    size_id BIGINT NULL,
    surface_treatment_id BIGINT NULL,
    is_active BIT NOT NULL CONSTRAINT df_manufactured_parts_is_active DEFAULT 1,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_manufactured_parts_drawing_no UNIQUE (drawing_no),
    CONSTRAINT fk_manufactured_parts_material FOREIGN KEY (material_id) REFERENCES materials(id),
    CONSTRAINT fk_manufactured_parts_shape FOREIGN KEY (shape_id) REFERENCES shapes(id),
    CONSTRAINT fk_manufactured_parts_processing_method FOREIGN KEY (processing_method_id) REFERENCES processing_methods(id),
    CONSTRAINT fk_manufactured_parts_size FOREIGN KEY (size_id) REFERENCES sizes(id),
    CONSTRAINT fk_manufactured_parts_surface_treatment FOREIGN KEY (surface_treatment_id) REFERENCES surface_treatments(id)
);

CREATE TABLE business_partner_materials (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    business_partner_id BIGINT NOT NULL,
    material_id BIGINT NOT NULL,
    priority INT NOT NULL CONSTRAINT df_business_partner_materials_priority DEFAULT 0,
    is_active BIT NOT NULL CONSTRAINT df_business_partner_materials_is_active DEFAULT 1,
    CONSTRAINT uq_business_partner_materials UNIQUE (business_partner_id, material_id),
    CONSTRAINT fk_business_partner_materials_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id),
    CONSTRAINT fk_business_partner_materials_material FOREIGN KEY (material_id) REFERENCES materials(id)
);

CREATE TABLE business_partner_shapes (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    business_partner_id BIGINT NOT NULL,
    shape_id BIGINT NOT NULL,
    priority INT NOT NULL CONSTRAINT df_business_partner_shapes_priority DEFAULT 0,
    is_active BIT NOT NULL CONSTRAINT df_business_partner_shapes_is_active DEFAULT 1,
    CONSTRAINT uq_business_partner_shapes UNIQUE (business_partner_id, shape_id),
    CONSTRAINT fk_business_partner_shapes_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id),
    CONSTRAINT fk_business_partner_shapes_shape FOREIGN KEY (shape_id) REFERENCES shapes(id)
);

CREATE TABLE business_partner_processing_methods (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    business_partner_id BIGINT NOT NULL,
    processing_method_id BIGINT NOT NULL,
    priority INT NOT NULL CONSTRAINT df_business_partner_processing_methods_priority DEFAULT 0,
    is_active BIT NOT NULL CONSTRAINT df_business_partner_processing_methods_is_active DEFAULT 1,
    CONSTRAINT uq_business_partner_processing_methods UNIQUE (business_partner_id, processing_method_id),
    CONSTRAINT fk_bp_processing_methods_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id),
    CONSTRAINT fk_bp_processing_methods_method FOREIGN KEY (processing_method_id) REFERENCES processing_methods(id)
);

CREATE TABLE business_partner_sizes (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    business_partner_id BIGINT NOT NULL,
    size_id BIGINT NOT NULL,
    priority INT NOT NULL CONSTRAINT df_business_partner_sizes_priority DEFAULT 0,
    is_active BIT NOT NULL CONSTRAINT df_business_partner_sizes_is_active DEFAULT 1,
    CONSTRAINT uq_business_partner_sizes UNIQUE (business_partner_id, size_id),
    CONSTRAINT fk_business_partner_sizes_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id),
    CONSTRAINT fk_business_partner_sizes_size FOREIGN KEY (size_id) REFERENCES sizes(id)
);

CREATE TABLE business_partner_surface_treatments (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    business_partner_id BIGINT NOT NULL,
    surface_treatment_id BIGINT NOT NULL,
    priority INT NOT NULL CONSTRAINT df_bp_surface_treatments_priority DEFAULT 0,
    is_active BIT NOT NULL CONSTRAINT df_bp_surface_treatments_is_active DEFAULT 1,
    CONSTRAINT uq_business_partner_surface_treatments UNIQUE (business_partner_id, surface_treatment_id),
    CONSTRAINT fk_bp_surface_treatments_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id),
    CONSTRAINT fk_bp_surface_treatments_surface FOREIGN KEY (surface_treatment_id) REFERENCES surface_treatments(id)
);

CREATE TABLE projects (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    project_no VARCHAR(11) NOT NULL,
    construction_project_id BIGINT NOT NULL,
    department_id BIGINT NULL,
    project_name NVARCHAR(200) NOT NULL,
    requester_user_id BIGINT NOT NULL,
    arranger_user_id BIGINT NOT NULL,
    project_status VARCHAR(30) NOT NULL,
    approval_status VARCHAR(20) NOT NULL,
    requested_at DATE NOT NULL,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_projects_project_no UNIQUE (project_no),
    CONSTRAINT fk_projects_construction_project FOREIGN KEY (construction_project_id) REFERENCES construction_projects(id),
    CONSTRAINT fk_projects_department FOREIGN KEY (department_id) REFERENCES departments(id),
    CONSTRAINT fk_projects_requester FOREIGN KEY (requester_user_id) REFERENCES users(id),
    CONSTRAINT fk_projects_arranger FOREIGN KEY (arranger_user_id) REFERENCES users(id)
);

CREATE TABLE project_items (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    project_id BIGINT NOT NULL,
    item_type VARCHAR(30) NOT NULL,
    item_id BIGINT NOT NULL,
    requested_qty DECIMAL(18,4) NOT NULL,
    unit NVARCHAR(50) NULL,
    usage_text NVARCHAR(200) NULL,
    requested_due_date DATE NULL,
    line_status VARCHAR(30) NOT NULL,
    adopted_quote_line_id BIGINT NULL,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_project_items UNIQUE (project_id, item_type, item_id),
    CONSTRAINT fk_project_items_project FOREIGN KEY (project_id) REFERENCES projects(id)
);

CREATE TABLE price_histories (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    item_type VARCHAR(30) NOT NULL,
    item_id BIGINT NOT NULL,
    business_partner_id BIGINT NOT NULL,
    price_category VARCHAR(50) NOT NULL,
    unit_price DECIMAL(18,4) NOT NULL,
    currency_code VARCHAR(10) NOT NULL CONSTRAINT df_price_histories_currency_code DEFAULT 'JPY',
    effective_from DATE NOT NULL,
    supplier_quote_no NVARCHAR(100) NULL,
    order_no NVARCHAR(100) NULL,
    source_type VARCHAR(50) NOT NULL,
    adopted_flag BIT NOT NULL CONSTRAINT df_price_histories_adopted_flag DEFAULT 0,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    CONSTRAINT fk_price_histories_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id)
);

CREATE TABLE quote_requests (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    project_item_id BIGINT NOT NULL,
    business_partner_id BIGINT NOT NULL,
    requested_on DATE NOT NULL,
    response_due_on DATE NULL,
    request_method VARCHAR(50) NULL,
    status VARCHAR(30) NOT NULL,
    note NVARCHAR(1000) NULL,
    CONSTRAINT fk_quote_requests_project_item FOREIGN KEY (project_item_id) REFERENCES project_items(id),
    CONSTRAINT fk_quote_requests_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id)
);

CREATE TABLE quotations (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    project_id BIGINT NOT NULL,
    business_partner_id BIGINT NOT NULL,
    internal_quote_no VARCHAR(50) NOT NULL,
    supplier_quote_no NVARCHAR(100) NULL,
    quote_date DATE NOT NULL,
    valid_until DATE NULL,
    total_amount DECIMAL(18,2) NOT NULL,
    approval_status VARCHAR(20) NOT NULL,
    adoption_status VARCHAR(20) NOT NULL,
    revision_no INT NOT NULL CONSTRAINT df_quotations_revision_no DEFAULT 1,
    root_quotation_id BIGINT NULL,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_quotations_internal_quote_no UNIQUE (internal_quote_no),
    CONSTRAINT fk_quotations_project FOREIGN KEY (project_id) REFERENCES projects(id),
    CONSTRAINT fk_quotations_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id),
    CONSTRAINT fk_quotations_root FOREIGN KEY (root_quotation_id) REFERENCES quotations(id)
);

CREATE TABLE quotation_lines (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    quotation_id BIGINT NOT NULL,
    project_item_id BIGINT NOT NULL,
    item_type VARCHAR(30) NOT NULL,
    item_id BIGINT NOT NULL,
    quoted_qty DECIMAL(18,4) NOT NULL,
    quoted_unit_price DECIMAL(18,4) NOT NULL,
    quoted_amount DECIMAL(18,2) NOT NULL,
    is_adopted BIT NOT NULL CONSTRAINT df_quotation_lines_is_adopted DEFAULT 0,
    is_ordered BIT NOT NULL CONSTRAINT df_quotation_lines_is_ordered DEFAULT 0,
    delivered_qty DECIMAL(18,4) NOT NULL CONSTRAINT df_quotation_lines_delivered_qty DEFAULT 0,
    checked_qty DECIMAL(18,4) NOT NULL CONSTRAINT df_quotation_lines_checked_qty DEFAULT 0,
    invoiced_qty DECIMAL(18,4) NOT NULL CONSTRAINT df_quotation_lines_invoiced_qty DEFAULT 0,
    is_completed BIT NOT NULL CONSTRAINT df_quotation_lines_is_completed DEFAULT 0,
    note NVARCHAR(1000) NULL,
    CONSTRAINT fk_quotation_lines_quotation FOREIGN KEY (quotation_id) REFERENCES quotations(id),
    CONSTRAINT fk_quotation_lines_project_item FOREIGN KEY (project_item_id) REFERENCES project_items(id)
);

ALTER TABLE project_items
ADD CONSTRAINT fk_project_items_adopted_quote_line FOREIGN KEY (adopted_quote_line_id) REFERENCES quotation_lines(id);

CREATE TABLE approval_route_definitions (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    department_id BIGINT NOT NULL,
    amount_rank_id BIGINT NOT NULL,
    step_no INT NOT NULL,
    required_approval_rank_id BIGINT NOT NULL,
    candidate_type VARCHAR(20) NOT NULL,
    candidate_user_id BIGINT NULL,
    candidate_ad_group_id BIGINT NULL,
    is_active BIT NOT NULL CONSTRAINT df_approval_route_definitions_is_active DEFAULT 1,
    CONSTRAINT uq_approval_route_definitions UNIQUE (department_id, amount_rank_id, step_no),
    CONSTRAINT fk_approval_route_definitions_department FOREIGN KEY (department_id) REFERENCES departments(id),
    CONSTRAINT fk_approval_route_definitions_amount_rank FOREIGN KEY (amount_rank_id) REFERENCES amount_ranks(id),
    CONSTRAINT fk_approval_route_definitions_required_rank FOREIGN KEY (required_approval_rank_id) REFERENCES approval_ranks(id),
    CONSTRAINT fk_approval_route_definitions_user FOREIGN KEY (candidate_user_id) REFERENCES users(id),
    CONSTRAINT fk_approval_route_definitions_ad_group FOREIGN KEY (candidate_ad_group_id) REFERENCES ad_group_links(id)
);

CREATE TABLE purchase_approvals (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    quotation_id BIGINT NOT NULL,
    department_id BIGINT NOT NULL,
    amount_rank_id BIGINT NOT NULL,
    required_approval_rank_id BIGINT NOT NULL,
    approval_step INT NOT NULL,
    requested_by_user_id BIGINT NOT NULL,
    approved_by_user_id BIGINT NULL,
    requested_on DATETIME2 NOT NULL,
    approved_on DATETIME2 NULL,
    approval_status VARCHAR(20) NOT NULL,
    comment NVARCHAR(1000) NULL,
    CONSTRAINT fk_purchase_approvals_quotation FOREIGN KEY (quotation_id) REFERENCES quotations(id),
    CONSTRAINT fk_purchase_approvals_department FOREIGN KEY (department_id) REFERENCES departments(id),
    CONSTRAINT fk_purchase_approvals_amount_rank FOREIGN KEY (amount_rank_id) REFERENCES amount_ranks(id),
    CONSTRAINT fk_purchase_approvals_required_rank FOREIGN KEY (required_approval_rank_id) REFERENCES approval_ranks(id),
    CONSTRAINT fk_purchase_approvals_requested_by FOREIGN KEY (requested_by_user_id) REFERENCES users(id),
    CONSTRAINT fk_purchase_approvals_approved_by FOREIGN KEY (approved_by_user_id) REFERENCES users(id)
);

CREATE TABLE orders (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    quotation_id BIGINT NOT NULL,
    business_partner_id BIGINT NOT NULL,
    order_no VARCHAR(11) NOT NULL,
    ordered_on DATE NOT NULL,
    total_amount DECIMAL(18,2) NOT NULL,
    planned_delivery_date DATE NULL,
    order_status VARCHAR(20) NOT NULL,
    cancelled_on DATE NULL,
    cancel_reason NVARCHAR(500) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT uq_orders_order_no UNIQUE (order_no),
    CONSTRAINT fk_orders_quotation FOREIGN KEY (quotation_id) REFERENCES quotations(id),
    CONSTRAINT fk_orders_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id)
);

CREATE TABLE order_lines (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    order_id BIGINT NOT NULL,
    quotation_line_id BIGINT NOT NULL,
    item_type VARCHAR(30) NOT NULL,
    item_id BIGINT NOT NULL,
    ordered_qty DECIMAL(18,4) NOT NULL,
    unit_price DECIMAL(18,4) NOT NULL,
    ordered_amount DECIMAL(18,2) NOT NULL,
    remaining_delivery_qty DECIMAL(18,4) NOT NULL,
    note NVARCHAR(1000) NULL,
    CONSTRAINT fk_order_lines_order FOREIGN KEY (order_id) REFERENCES orders(id),
    CONSTRAINT fk_order_lines_quotation_line FOREIGN KEY (quotation_line_id) REFERENCES quotation_lines(id)
);

CREATE TABLE deliveries (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    order_id BIGINT NOT NULL,
    delivery_note_no NVARCHAR(100) NOT NULL,
    delivered_on DATE NOT NULL,
    delivery_status VARCHAR(20) NOT NULL,
    checked_on DATE NULL,
    checked_by_user_id BIGINT NULL,
    invoice_target_month CHAR(7) NULL,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT fk_deliveries_order FOREIGN KEY (order_id) REFERENCES orders(id),
    CONSTRAINT fk_deliveries_checked_by FOREIGN KEY (checked_by_user_id) REFERENCES users(id)
);

CREATE TABLE delivery_lines (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    delivery_id BIGINT NOT NULL,
    order_line_id BIGINT NOT NULL,
    quotation_line_id BIGINT NOT NULL,
    item_type VARCHAR(30) NOT NULL,
    item_id BIGINT NOT NULL,
    delivered_qty DECIMAL(18,4) NOT NULL,
    accepted_qty DECIMAL(18,4) NOT NULL,
    difference_qty DECIMAL(18,4) NOT NULL CONSTRAINT df_delivery_lines_difference_qty DEFAULT 0,
    check_result VARCHAR(30) NOT NULL,
    note NVARCHAR(1000) NULL,
    CONSTRAINT fk_delivery_lines_delivery FOREIGN KEY (delivery_id) REFERENCES deliveries(id),
    CONSTRAINT fk_delivery_lines_order_line FOREIGN KEY (order_line_id) REFERENCES order_lines(id),
    CONSTRAINT fk_delivery_lines_quotation_line FOREIGN KEY (quotation_line_id) REFERENCES quotation_lines(id)
);

CREATE TABLE invoices (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    quotation_id BIGINT NOT NULL,
    business_partner_id BIGINT NOT NULL,
    invoice_no NVARCHAR(100) NOT NULL,
    invoiced_on DATE NOT NULL,
    total_amount DECIMAL(18,2) NOT NULL,
    billing_period NVARCHAR(50) NULL,
    invoice_status VARCHAR(20) NOT NULL,
    note NVARCHAR(1000) NULL,
    created_at DATETIME2 NOT NULL,
    updated_at DATETIME2 NOT NULL,
    CONSTRAINT fk_invoices_quotation FOREIGN KEY (quotation_id) REFERENCES quotations(id),
    CONSTRAINT fk_invoices_partner FOREIGN KEY (business_partner_id) REFERENCES business_partners(id)
);

CREATE TABLE invoice_targets (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    invoice_id BIGINT NOT NULL,
    delivery_line_id BIGINT NOT NULL,
    invoiced_qty DECIMAL(18,4) NOT NULL,
    invoiced_amount DECIMAL(18,2) NOT NULL,
    CONSTRAINT uq_invoice_targets UNIQUE (invoice_id, delivery_line_id),
    CONSTRAINT fk_invoice_targets_invoice FOREIGN KEY (invoice_id) REFERENCES invoices(id),
    CONSTRAINT fk_invoice_targets_delivery_line FOREIGN KEY (delivery_line_id) REFERENCES delivery_lines(id)
);

CREATE TABLE document_forwardings (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    project_id BIGINT NOT NULL,
    document_type VARCHAR(30) NOT NULL,
    target_record_id BIGINT NULL,
    forwarded_on DATE NOT NULL,
    forwarded_to NVARCHAR(200) NULL,
    forwarded_by_user_id BIGINT NULL,
    note NVARCHAR(1000) NULL,
    CONSTRAINT fk_document_forwardings_project FOREIGN KEY (project_id) REFERENCES projects(id),
    CONSTRAINT fk_document_forwardings_user FOREIGN KEY (forwarded_by_user_id) REFERENCES users(id)
);

CREATE TABLE attachment_links (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    target_type VARCHAR(30) NOT NULL,
    target_record_id BIGINT NOT NULL,
    document_type VARCHAR(30) NOT NULL,
    file_name NVARCHAR(255) NOT NULL,
    file_link NVARCHAR(1000) NOT NULL,
    created_by_user_id BIGINT NULL,
    created_at DATETIME2 NOT NULL,
    note NVARCHAR(1000) NULL,
    CONSTRAINT fk_attachment_links_user FOREIGN KEY (created_by_user_id) REFERENCES users(id)
);

CREATE TABLE notifications (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    notification_type VARCHAR(50) NOT NULL,
    channel_type VARCHAR(20) NOT NULL,
    related_project_id BIGINT NULL,
    related_record_type VARCHAR(50) NULL,
    related_record_id BIGINT NULL,
    subject NVARCHAR(200) NOT NULL,
    body NVARCHAR(2000) NULL,
    triggered_at DATETIME2 NOT NULL,
    sent_at DATETIME2 NULL,
    status VARCHAR(30) NOT NULL,
    CONSTRAINT fk_notifications_project FOREIGN KEY (related_project_id) REFERENCES projects(id)
);

CREATE TABLE notification_recipients (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    notification_id BIGINT NOT NULL,
    user_id BIGINT NULL,
    email VARCHAR(255) NULL,
    delivered_at DATETIME2 NULL,
    read_at DATETIME2 NULL,
    read_flag BIT NOT NULL CONSTRAINT df_notification_recipients_read_flag DEFAULT 0,
    CONSTRAINT uq_notification_recipients UNIQUE (notification_id, user_id),
    CONSTRAINT fk_notification_recipients_notification FOREIGN KEY (notification_id) REFERENCES notifications(id),
    CONSTRAINT fk_notification_recipients_user FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE audit_logs (
    id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    table_name VARCHAR(100) NOT NULL,
    record_id BIGINT NOT NULL,
    operation_type VARCHAR(20) NOT NULL,
    operated_at DATETIME2 NOT NULL,
    operator_user_id BIGINT NULL,
    before_json NVARCHAR(MAX) NULL,
    after_json NVARCHAR(MAX) NULL,
    reason NVARCHAR(500) NULL,
    CONSTRAINT fk_audit_logs_user FOREIGN KEY (operator_user_id) REFERENCES users(id)
);

CREATE INDEX ix_projects_construction_project_id ON projects(construction_project_id);
CREATE INDEX ix_projects_project_status ON projects(project_status);
CREATE INDEX ix_project_items_project_id ON project_items(project_id);
CREATE INDEX ix_quotations_project_id ON quotations(project_id);
CREATE INDEX ix_quotations_business_partner_id ON quotations(business_partner_id);
CREATE INDEX ix_quotation_lines_quotation_id ON quotation_lines(quotation_id);
CREATE INDEX ix_orders_quotation_id ON orders(quotation_id);
CREATE INDEX ix_order_lines_order_id ON order_lines(order_id);
CREATE INDEX ix_deliveries_order_id ON deliveries(order_id);
CREATE INDEX ix_delivery_lines_delivery_id ON delivery_lines(delivery_id);
CREATE INDEX ix_invoices_quotation_id ON invoices(quotation_id);
CREATE INDEX ix_document_forwardings_project_id ON document_forwardings(project_id);
CREATE INDEX ix_notifications_related_project_id ON notifications(related_project_id);
CREATE INDEX ix_audit_logs_table_name_record_id ON audit_logs(table_name, record_id);
